namespace Boek.Infrastructure.Requests.BookProducts
{
    public class UpdateBookProductStartedCampaignRequestModel
    {
        public Guid Id { get; set; }
        public int SaleQuantity { get; set; }
        public byte? Status { get; set; }
    }
}