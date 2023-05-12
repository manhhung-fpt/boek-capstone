using Boek.Core.Data;
using Boek.Core.Entities;
using Boek.Repository.Interfaces;

namespace Boek.Repository.Repositories
{
    public class BookProductRepository : GenericRepository<BookProduct>, IBookProductRepository
    {
        public BookProductRepository(BoekCapstoneContext context) : base(context)
        {
        }

        public BookProduct CheckDuplicatedComboName(string title, int campaignId)
        => dbSet.SingleOrDefault(bp =>
            bp.Title.ToLower().Trim().Equals(title.ToLower().Trim()) &&
            bp.CampaignId.Equals(campaignId));
    }
}
