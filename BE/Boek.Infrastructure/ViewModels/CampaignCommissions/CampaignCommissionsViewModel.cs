using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.ViewModels.Genres;

namespace Boek.Infrastructure.ViewModels.CampaignCommissions
{
    public class CampaignCommissionsViewModel
    {
        [Int]
        public int? Id { get; set; }

        [Int]
        public int? CampaignId { get; set; }

        [Int]
        public int? GenreId { get; set; }

        [Int]
        public int? MinimalCommission { get; set; }

        public GenreViewModel Genre { get; set; }
    }
}
