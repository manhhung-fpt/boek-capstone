namespace Boek.Infrastructure.Requests.CampaignCommissions
{
    public class CreateCampaignCommissionRequestModel
    {
        public int? CampaignId { get; set; }
        public int? GenreId { get; set; }
        public int? MinimalCommission { get; set; }
    }
}
