namespace Boek.Infrastructure.Requests.CampaignStaffs
{
    public class CreateCampaignStaffRequestModel
    {
        public int? CampaignId { get; set; }

        public List<Guid?> StaffIds { get; set; }
    }
}
