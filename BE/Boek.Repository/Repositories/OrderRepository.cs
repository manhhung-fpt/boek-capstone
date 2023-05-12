using Boek.Core.Data;
using Boek.Core.Entities;
using Boek.Core.Enums;
using Boek.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Boek.Repository.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(BoekCapstoneContext context) : base(context)
        {
        }

        public double GetTotal(Order order)
        {
            double orderTotal = 0;
            if (order.OrderDetails != null)
            {
                if (order.OrderDetails.Any())
                {
                    var checkPriceItems = order.OrderDetails.GroupBy(od => od.Price.HasValue);
                    if (checkPriceItems.Any(cpis =>
                    cpis.Key.Equals(true) &&
                    cpis.Select(od => od).Count().Equals(order.OrderDetails.Count())))
                    {
                        order.OrderDetails.ToList().ForEach(od =>
                        {
                            double temp = (double)(od.Price * od.Quantity);
                            if (od.BookProduct.Type.Equals((byte)BookType.Odd))
                                temp += GetPdfOrAudioPriceOddBook(od);
                            else
                                temp += GetPdfOrAudioPriceSeriesOrComboBook(od);
                            orderTotal += temp;
                        });
                        if (order.Type.Equals((byte)OrderType.Shipping))
                        {
                            var freight = order.Freight ?? 0;
                            orderTotal += (double)freight;
                        }
                    }
                }
            }
            return orderTotal;
        }

        public double GetSubTotal(Order order)
        {
            double orderTotal = 0;
            if (order.OrderDetails != null)
            {
                if (order.OrderDetails.Any())
                {
                    var checkPriceItems = order.OrderDetails.GroupBy(od => od.Price.HasValue);
                    if (checkPriceItems.Any(cpis =>
                    cpis.Key.Equals(true) &&
                    cpis.Select(od => od).Count().Equals(order.OrderDetails.Count())))
                    {
                        order.OrderDetails.ToList().ForEach(od =>
                        {
                            decimal price = 0;
                            if (od.BookProduct.Type.Equals((byte)BookType.Combo))
                                price = (decimal)od.Price;
                            else
                                price = od.BookProduct.Book.CoverPrice;
                            double temp = (double)(price * od.Quantity);
                            if (od.BookProduct.Type.Equals((byte)BookType.Odd))
                                temp += GetPdfOrAudioPriceOddBook(od);
                            else
                                temp += GetPdfOrAudioPriceSeriesOrComboBook(od);
                            orderTotal += temp;
                        });
                    }
                }
            }
            return orderTotal;
        }

        private double GetPdfOrAudioPriceOddBook(OrderDetail orderDetail)
        => (bool)orderDetail.WithAudio ?
            (double)(orderDetail.BookProduct.AudioExtraPrice * orderDetail.Quantity) : 0 +
            ((bool)orderDetail.WithPdf ?
            (double)(orderDetail.BookProduct.PdfExtraPrice * orderDetail.Quantity) : 0);

        private double GetPdfOrAudioPriceSeriesOrComboBook(OrderDetail orderDetail)
        {
            double result = 0;
            if ((bool)orderDetail.WithAudio)
                result += GetPriceOfBookProductItems(orderDetail.BookProduct.BookProductItems.ToList(), orderDetail.Quantity);
            if ((bool)orderDetail.WithPdf)
                result += GetPriceOfBookProductItems(orderDetail.BookProduct.BookProductItems.ToList(), orderDetail.Quantity, false);
            return result;
        }

        private double GetPriceOfBookProductItems(List<BookProductItem> bookProductItems, int Quantity, bool WithAudio = true)
        {
            double result = 0;
            if (WithAudio)
                bookProductItems.ForEach(bpi => result += (double)(bpi.AudioExtraPrice * Quantity));
            else
                bookProductItems.ForEach(bpi => result += (double)(bpi.PdfExtraPrice * Quantity));
            return result;
        }

        public async Task<Order> AddAsyncCustom(Order entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            var createdEntity = await _context.Orders.FindAsync(entity.Id);
            if (createdEntity == null)
            {
                throw new ApplicationException("Thao tác lỗi. Vui lòng liên hệ quản trị viên!");
            }
            return createdEntity;
        }

        public IQueryable<Order> GetOrdersByCustomer(Guid? id)
        {
            if (id == Guid.Empty)
                throw new ApplicationException("Id is null");
            return _context.Orders.Include(e => e.Customer).Where(q => q.Id == id).AsQueryable();
        }
    }
}
