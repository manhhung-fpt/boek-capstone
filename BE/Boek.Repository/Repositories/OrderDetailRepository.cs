using Boek.Core.Data;
using Boek.Core.Entities;
using Boek.Core.Enums;
using Boek.Repository.Interfaces;

namespace Boek.Repository.Repositories
{
    public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(BoekCapstoneContext context) : base(context)
        {
        }

        public double GetSubTotal(OrderDetail orderDetail)
        {
            double orderTotal = 0;
            if (orderDetail.Price.HasValue)
            {
                decimal price = 0;
                if (orderDetail.BookProduct.Type.Equals((byte)BookType.Combo))
                    price = (decimal)orderDetail.Price;
                else
                    price = orderDetail.BookProduct.Book.CoverPrice;
                double temp = (double)(price * orderDetail.Quantity);
                if (orderDetail.BookProduct.Type.Equals((byte)BookType.Odd))
                    temp += GetPdfOrAudioPriceOddBook(orderDetail);
                else
                    temp += GetPdfOrAudioPriceSeriesOrComboBook(orderDetail);
                orderTotal += temp;
            }
            return orderTotal;
        }

        public double GetTotal(OrderDetail orderDetail)
        {
            double orderTotal = 0;
            if (orderDetail != null)
            {
                if (orderDetail.Price.HasValue)
                {
                    double temp = (double)(orderDetail.Price * orderDetail.Quantity);
                    if (orderDetail.BookProduct.Type.Equals((byte)BookType.Odd))
                        temp += GetPdfOrAudioPriceOddBook(orderDetail);
                    else
                        temp += GetPdfOrAudioPriceSeriesOrComboBook(orderDetail);
                    orderTotal = temp;
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

    }
}
