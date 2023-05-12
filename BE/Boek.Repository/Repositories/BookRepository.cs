using Boek.Core.Data;
using Boek.Core.Entities;
using Boek.Core.Enums;
using Boek.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Boek.Repository.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(BoekCapstoneContext context) : base(context)
        {
        }

        public Book CheckDuplicatedBookName(string name)
        {
            var book = dbSet.SingleOrDefault(u =>
            u.Name.ToLower().Trim().Equals(name.ToLower().Trim()));
            return book;
        }

        public List<Campaign> GetCampaigns(int id)
        {
            var list = new List<Campaign>();
            var book = dbSet.Where(b => b.Id.Equals(id))
            .Include(b => b.BookProducts)
            .ThenInclude(bookProduct => bookProduct.Campaign)
            .Include(b => b.BookProductItems)
            .ThenInclude(bookProductItem => bookProductItem.ParentBookProduct)
            .ThenInclude(ParentBookProduct => ParentBookProduct.Campaign)
            .SingleOrDefault();
            if (book != null)
            {
                if (book.BookProducts.Any())
                {
                    var campaigns = book.BookProducts.Select(bp => bp.Campaign)
                        .Where(c => !list.Contains(c) && c.Status.Equals((byte)CampaignStatus.Start));
                    if (campaigns.Any())
                        list.AddRange(campaigns);
                }
                if (book.BookProductItems.Any())
                {
                    var campaigns = book.BookProductItems
                        .Select(bp => bp.ParentBookProduct.Campaign)
                        .Where(c => !list.Contains(c) && c.Status.Equals((byte)CampaignStatus.Start));
                    if (campaigns.Any())
                        list.AddRange(campaigns);
                }
            }
            return list;
        }

        public bool IsAllowChangingGenre(int id)
        {
            var result = true;
            var book = dbSet.Where(b => b.Id.Equals(id))
            .Include(b => b.BookItemBooks)
            .Include(b => b.BookProducts)
            .ThenInclude(bookProduct => bookProduct.Campaign)
            .Include(b => b.BookProducts)
            .ThenInclude(bookProduct => bookProduct.OrderDetails)
            .ThenInclude(orderDetail => orderDetail.Order)
            .SingleOrDefault();
            if (book != null)
            {
                var result1 = CheckBookGenreByCampaign(book);
                var result2 = CheckBookGenreByOrder(book);
                result = result1 || result2;
                if (!(bool)book.IsSeries)
                {
                    var result3 = CheckBookGenreBySeriesParentBook(book);
                    result = result || result3;
                }
            }
            return !result;
        }

        /// <summary>
        /// Check book by series book parent
        /// </summary>
        /// <param name="book"></param>
        /// <returns>
        /// Return true if there is series book parent. Otherwise, it returns false
        /// </returns>
        private bool CheckBookGenreBySeriesParentBook(Book book)
        => book.BookItemBooks.Any();

        /// <summary>
        /// Check book by started campaign and book product's status
        /// </summary>
        /// <param name="book"></param>
        /// <returns>
        /// Return true if there is book product is sale in a started campaigns. Otherwise, it returns false
        /// </returns>
        private bool CheckBookGenreByCampaign(Book book)
        {
            var result = false;
            if (book.BookProducts.Any())
            {
                var validBookProductStatus = new List<byte?>()
                {
                    (byte)BookProductStatus.Sale,
                    (byte)BookProductStatus.OutOfStock
                };
                result = book.BookProducts.Any(bp => validBookProductStatus.Contains(bp.Status)
                && bp.Campaign.Status.Equals((byte)CampaignStatus.Start));
            }
            return result;
        }
        /// <summary>
        /// Check book by order's status
        /// </summary>
        /// <param name="book"></param>
        /// <returns>
        /// Return true if there is matching order's status. Otherwise, it returns false
        /// </returns>
        private bool CheckBookGenreByOrder(Book book)
        {
            var result = false;
            if (book.BookProducts.Any(bp => bp.OrderDetails.Any()))
            {
                var validOrderStatus = new List<byte?>()
                {
                    (byte)OrderStatus.PickUpAvailable,
                    (byte)OrderStatus.Shipping
                };
                result = book.BookProducts.Any(bp =>
                bp.OrderDetails.Any(od =>
                validOrderStatus.Contains(od.Order.Status)));
            }
            return result;
        }
    }
}
