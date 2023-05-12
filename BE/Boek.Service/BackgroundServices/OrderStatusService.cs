using Boek.Core.Constants;
using Boek.Core.Entities;
using Boek.Core.Enums;
using Boek.Core.Extensions;
using Boek.Repository.Interfaces;
using Boek.Service.Interfaces;
using Boek.Service.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Boek.Service.BackgroundServices
{
    public class OrderStatusService : BackgroundService
    {
        #region Fields and constructor
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderStatusService> _logger;
        private Timer _timer = null;

        public OrderStatusService(IServiceProvider services, ILogger<OrderStatusService> logger)
        {
            var scope = services.CreateScope();
            _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            _orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
            _logger = logger;
        }
        #endregion

        #region Background service
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(2));
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            try
            {
                var invalidStatus = new List<byte?>()
                {
                    (byte) OrderStatus.Shipped,
                    (byte) OrderStatus.Received,
                    (byte) OrderStatus.Cancelled,
                };
                var orders = await _unitOfWork.Orders
                    .Get(o => !invalidStatus.Contains(o.Status))
                    .Include(o => o.Campaign)
                    .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.BookProduct)
                    .ToListAsync();
                if (orders.Any())
                {
                    var cancelledOrders = new List<Guid>();
                    _logger.LogInformation("============= Order Information =============");
                    //Expired pending order
                    CheckOrdersByExpiredPendingOrders(orders, ref cancelledOrders);
                    //Book products are disabled
                    CheckOrdersByDisabledBookProducts(orders, ref cancelledOrders);
                    //Book products are not sale
                    CheckOrdersByNotSaleBookProducts(orders, ref cancelledOrders);
                    //Campaigns end
                    CheckOrdersByEndCampaign(orders, ref cancelledOrders);
                    //Campaigns are postponed
                    CheckOrdersByPostponedCampaign(orders, ref cancelledOrders);
                    if (cancelledOrders.Any())
                        _orderService.SendBoekCancelledOrderEmail(cancelledOrders);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
        #endregion

        #region Utils
        private void CheckOrdersByExpiredPendingOrders(List<Order> orders, ref List<Guid> list)
        {
            //Get expired pending orders
            //1. Status is pending
            //2. Order date is after 15 minutes from creating
            var dateTimeNow = DateTime.Now;
            orders = orders.Where(o =>
            o.Status.Equals((byte)OrderStatus.Pending) &&
            DateTime.Compare((((DateTime)o.OrderDate).AddMinutes(15)), dateTimeNow) <= 0)
            .ToList();
            if (orders.Any())
            {
                var status = OrderStatus.Cancelled;
                var updateOrders = UpdateOrderStatusByOrder(orders, (byte)status, ErrorMessageConstants.ORDER_CANCELLED_STATUS_EXPIRED_PENDING_ORDER_MESS_DETAIL + " " + ErrorMessageConstants.ORDER_WITH_ORDER_CODE);
                UpdateOrders(updateOrders, ErrorMessageConstants.ORDER_CANCELLED_STATUS_EXPIRED_PENDING_ORDER_MESS);
                list.AddRange(orders.Select(o => o.Id));
            }
        }
        private void CheckOrdersByDisabledBookProducts(List<Order> orders, ref List<Guid> list)
        {
            var bookProductStatus = (byte)BookProductStatus.Unreleased;
            orders = orders.Where(o =>
            o.Campaign.Status.Equals((byte)CampaignStatus.Start) &&
            o.OrderDetails.Any(od =>
            od.BookProduct.Status.Equals(bookProductStatus)))
            .ToList();
            if (orders.Any())
            {
                var status = OrderStatus.Cancelled;
                var updateOrders = UpdateOrderStatusByBookProduct(orders, (byte)status, bookProductStatus, ErrorMessageConstants.ORDER_CANCELLED_STATUS_DISABLED_BOOK_PRODUCT_MESS + " " + ErrorMessageConstants.ORDER_WITH_BOOK_PRODUCT_ID);
                UpdateOrders(updateOrders, ErrorMessageConstants.ORDER_CANCELLED_STATUS_DISABLED_BOOK_PRODUCT_MESS);
                list.AddRange(orders.Select(o => o.Id));
            }
        }
        private void CheckOrdersByNotSaleBookProducts(List<Order> orders, ref List<Guid> list)
        {
            var bookProductStatus = (byte)BookProductStatus.NotSale;
            orders = orders.Where(o =>
            o.Campaign.Status.Equals((byte)CampaignStatus.Start) &&
            o.OrderDetails.Any(od =>
            od.BookProduct.Status.Equals(bookProductStatus)))
            .ToList();
            if (orders.Any())
            {
                var status = OrderStatus.Cancelled;
                var updateOrders = UpdateOrderStatusByBookProduct(orders, (byte)status, bookProductStatus, ErrorMessageConstants.ORDER_CANCELLED_STATUS_NOT_SALE_BOOK_PRODUCT_MESS + " " + ErrorMessageConstants.ORDER_WITH_BOOK_PRODUCT_ID);
                UpdateOrders(updateOrders, ErrorMessageConstants.ORDER_CANCELLED_STATUS_NOT_SALE_BOOK_PRODUCT_MESS);
                list.AddRange(orders.Select(o => o.Id));
            }
        }
        private void CheckOrdersByEndCampaign(List<Order> orders, ref List<Guid> list)
        {
            orders = orders.Where(o =>
            o.Campaign.Status.Equals((byte)CampaignStatus.End) || IsEndCampaign(o.Campaign))
            .ToList();
            if (orders.Any())
            {
                var status = OrderStatus.Cancelled;
                var updateOrders = UpdateOrderStatusByCampaign(orders, (byte)status, ErrorMessageConstants.ORDER_CANCELLED_STATUS_END_CAMPAIGN_MESS + " " + ErrorMessageConstants.ORDER_WITH_CAMPAIGN_ID);
                UpdateOrders(updateOrders, ErrorMessageConstants.ORDER_CANCELLED_STATUS_END_CAMPAIGN_MESS);
                list.AddRange(orders.Select(o => o.Id));
            }
        }

        private bool IsEndCampaign(Campaign campaign)
        => DateTime.Compare((DateTime)campaign.StartDate, DateTime.Now) < 0 &&
        DateTime.Compare((DateTime)campaign.EndDate, DateTime.Now) <= 0;

        private void CheckOrdersByPostponedCampaign(List<Order> orders, ref List<Guid> list)
        {
            orders = orders.Where(o =>
            o.Campaign.Status.Equals((byte)CampaignStatus.Postponed))
            .ToList();
            if (orders.Any())
            {
                var status = OrderStatus.Cancelled;
                var updateOrders = UpdateOrderStatusByCampaign(orders, (byte)status, ErrorMessageConstants.ORDER_CANCELLED_STATUS_POSTPONED_CAMPAIGN_MESS + " " + ErrorMessageConstants.ORDER_WITH_CAMPAIGN_ID);
                UpdateOrders(updateOrders, ErrorMessageConstants.ORDER_CANCELLED_STATUS_POSTPONED_CAMPAIGN_MESS);
                list.AddRange(orders.Select(o => o.Id));
            }
        }

        private List<Order> UpdateOrderStatusByOrder(List<Order> orders, byte status, string cancelledMessage)
        {
            orders.ForEach(o =>
            {
                o.Status = status;
                string message = o.Code;
                var payment = StatusExtension<OrderPayment>.GetStatus(o.Payment).ToLower();
                message = cancelledMessage + " " + message;
                if (string.IsNullOrEmpty(payment))
                    message = message.Replace(" [Payment] ", " ");
                else
                    message = message.Replace("[Payment]", payment);
                o.Note = ServiceUtils.GetUpdateNote(message, o.Note, MessageConstants.BOEK);
                o.CancelledDate = DateTime.Now;
            });
            return orders;
        }

        private Dictionary<Guid?, int> UpdateBookProductByPendingOrder(List<Order> orders)
        {
            Dictionary<Guid?, int> orderDetails = new Dictionary<Guid?, int>();
            orders.SelectMany(o => o.OrderDetails).
            GroupBy(od => od.BookProductId)
            .Select(bps => new
            {
                bp = bps.Key,
                quantity = bps.Sum(od => od.Quantity)
            }).ToList().ForEach(item =>
            {
                if (item.quantity > 0)
                    orderDetails.Add(item.bp, item.quantity);
            });
            return orderDetails.Any() ? orderDetails : null;
        }

        private List<Order> UpdateOrderStatusByBookProduct(List<Order> orders, byte status, byte bookProductStatus, string cancelledMessage)
        {
            orders.ForEach(o =>
            {
                o.Status = status;
                string message = "";
                var bookProductIds = o.OrderDetails.Where(od =>
                    od.BookProduct.Status.Equals(bookProductStatus))
                    .Select(od => od.BookProductId).ToList();
                bookProductIds.ForEach(bpi =>
                {
                    if (string.IsNullOrEmpty(message))
                        message = bpi.ToString();
                    else
                        message += $", {bpi}";
                });
                message = cancelledMessage + " " + message;
                o.Note = ServiceUtils.GetUpdateNote(message, o.Note, MessageConstants.BOEK);
                o.CancelledDate = DateTime.Now;
            });
            return orders;
        }

        private List<Order> UpdateOrderStatusByCampaign(List<Order> orders, byte status, string cancelledMessage)
        {
            orders.ForEach(o =>
            {
                o.Status = status;
                string message = o.CampaignId.ToString();
                message = cancelledMessage + " " + message;
                o.Note = ServiceUtils.GetUpdateNote(message, o.Note, MessageConstants.BOEK);
                o.CancelledDate = DateTime.Now;
            });
            return orders;
        }

        private void UpdateOrders(List<Order> list, string message)
        {
            _logger.LogInformation($"[>> {message}]");
            list.ForEach(async o =>
                {
                    await _unitOfWork.Orders.UpdateAsync(o);
                    if (_unitOfWork.Save())
                        _logger.LogInformation($"[Change] : <{o.Id}> - {o.OrderDate} - {o.CustomerName}");
                    else
                        _logger.LogError($"[Error] : <{o.Id}> - {o.OrderDate} - {o.CustomerName}");
                });
        }
        #endregion
    }
}