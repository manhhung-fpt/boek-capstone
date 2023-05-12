using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.Requests.CampaignCommissions
{
    public class CampaignCommissionRequestModel
    {
        [Int]
        public int? Id { get; set; }

        [Int]
        public int? CampaignId { get; set; }

        [Int]
        public int? GenreId { get; set; }

        [Int]
        public int? MinimalCommission { get; set; }
    }
}
