using Boek.Core.Entities;
using Boek.Repository.Interfaces;
using Boek.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Boek.Core.Extensions;

namespace Boek.Service.BackgroundServices
{
    public class BookProductStatusService : BackgroundService
    {
        #region Fields and constructor
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BookProductStatusService> _logger;
        private Timer _timer = null;

        public BookProductStatusService(IServiceProvider services, ILogger<BookProductStatusService> logger)
        {
            var scope = services.CreateScope();
            _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            _logger = logger;
        }
        #endregion

        #region Background service
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(3));
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            try
            {
                var invalidStatus = new List<byte?>()
                {
                    (byte)BookProductStatus.Rejected,
                    (byte)BookProductStatus.Pending
                };
                var bookProducts = await _unitOfWork.BookProducts.Get(bp => !invalidStatus.Contains(bp.Status))
                .Include(bp => bp.Book)
                .Include(bp => bp.Campaign)
                .ToListAsync();
                if (bookProducts.Any())
                {
                    _logger.LogInformation("============= Book Product Information =============");
                    //Sale
                    CheckSaleBookProduct(bookProducts);
                    //Not sale due date
                    CheckNotSaleDueEndDateBookProduct(bookProducts);
                    //Not sale due cancelled campaign
                    CheckNotSaleDueCancelledCampaignBookProduct(bookProducts);
                    //Not sale due postponed campaign
                    CheckNotSaleDuePostponedCampaignBookProduct(bookProducts);
                    //Sale due restarted campaign
                    CheckSaleDueRestartedCampaignBookProduct(bookProducts);
                    //Out of stock
                    CheckOutOfStockDueDateBookProduct(bookProducts);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
        #endregion

        #region Utils
        private void CheckSaleBookProduct(List<BookProduct> bookProducts)
        {
            var list = bookProducts.Where(bp => bp.SaleQuantity > 0 &&
            !bp.Status.Equals((byte)BookProductStatus.Unreleased) &&
            !bp.Status.Equals((byte)BookProductStatus.NotSale) &&
            bp.Campaign.Status.Equals((byte)CampaignStatus.Start)).ToList();
            if (list.Any())
                UpdateBookProducts(list, BookProductStatus.Sale);
        }
        private void CheckNotSaleDueEndDateBookProduct(List<BookProduct> bookProducts)
        {
            var list = bookProducts.Where(bp =>
            bp.Campaign.Status.Equals((byte)CampaignStatus.End)).ToList();

            var status = BookProductStatus.NotSaleDueEndDate;

            if (list.Any())
                UpdateBookProducts(list, status);
        }
        private void CheckNotSaleDueCancelledCampaignBookProduct(List<BookProduct> bookProducts)
        {
            var list = bookProducts.Where(bp =>
            bp.Campaign.Status.Equals((byte)CampaignStatus.Cancelled)).ToList();

            var status = BookProductStatus.NotSaleDueCancelledCampaign;

            if (list.Any())
                UpdateBookProducts(list, status);
        }
        private void CheckNotSaleDuePostponedCampaignBookProduct(List<BookProduct> bookProducts)
        {
            var ValidList = new List<byte?>()
            {
                (byte)BookProductStatus.Sale,
                (byte)BookProductStatus.OutOfStock,
            };
            var list = bookProducts.Where(bp =>
            bp.Campaign.Status.Equals((byte)CampaignStatus.Postponed) &&
            ValidList.Contains(bp.Status)).ToList();

            var status = BookProductStatus.NotSaleDuePostponedCampaign;

            if (list.Any())
                UpdateBookProducts(list, status);
        }
        private void CheckSaleDueRestartedCampaignBookProduct(List<BookProduct> bookProducts)
        {
            var ValidList = new List<byte?>() { (byte)BookProductStatus.NotSaleDuePostponedCampaign };
            var list = bookProducts.Where(bp =>
            bp.Campaign.Status.Equals((byte)CampaignStatus.Start) &&
            ValidList.Contains(bp.Status)).ToList();

            if (list.Any())
            {
                var status = BookProductStatus.Unreleased;
                //Unreleased
                var tempList = list.Where(bp => bp.Book.Status.Equals(BookStatus.Unreleased)).ToList();
                if (tempList.Any())
                    UpdateBookProducts(tempList, status);
                //Out of stock
                tempList = list.Where(bp => bp.SaleQuantity <= 0 && bp.Book.Status.Equals(BookStatus.Releasing)).ToList();
                if (tempList.Any())
                {
                    status = BookProductStatus.OutOfStock;
                    UpdateBookProducts(tempList, status);
                }
                //Sale 
                tempList = list.Where(bp => bp.SaleQuantity > 0 && bp.Book.Status.Equals(BookStatus.Releasing)).ToList();
                if (tempList.Any())
                {
                    status = BookProductStatus.Sale;
                    UpdateBookProducts(tempList, status);
                }
            }
        }
        private void CheckOutOfStockDueDateBookProduct(List<BookProduct> bookProducts)
        {
            var validStatus = new List<byte?>()
            {
                (byte)CampaignStatus.NotStarted,
                (byte)CampaignStatus.Start
            };

            var list = bookProducts.Where(bp =>
            bp.SaleQuantity <= 0 &&
            validStatus.Contains(bp.Campaign.Status)).ToList();

            var status = BookProductStatus.OutOfStock;

            if (list.Any())
                UpdateBookProducts(list, status);
        }

        private void UpdateBookProducts(List<BookProduct> list, BookProductStatus status)
        {
            var statusName = StatusExtension<BookProductStatus>.GetStatus((byte)status);
            _logger.LogInformation($"[>> {statusName}]");
            list.ForEach(async bp =>
                {
                    if (!bp.Status.Equals((byte)status))
                    {
                        bp.Status = (byte)status;
                        await _unitOfWork.BookProducts.UpdateAsync(bp);
                        if (_unitOfWork.Save())
                            _logger.LogInformation($"[Change] : <{bp.Id}> - {bp.Title}");
                        else
                            _logger.LogError($"[Error] : <{bp.Id}> - {bp.Title}");
                    }
                });
        }
        #endregion
    }
}