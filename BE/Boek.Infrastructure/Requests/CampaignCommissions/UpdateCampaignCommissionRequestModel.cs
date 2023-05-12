namespace Boek.Infrastructure.Requests.CampaignCommissions
{
    public class UpdateCampaignCommissionRequestModel
    {
        public int Id { get; set; }
        public int? CampaignId { get; set; }
        public int? GenreId { get; set; }
        public int? MinimalCommission { get; set; }
    }
}
