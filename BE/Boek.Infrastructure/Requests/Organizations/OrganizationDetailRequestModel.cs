namespace Boek.Infrastructure.Requests.Organizations
{
    public class OrganizationDetailRequestModel
    {
        public bool? WithCustomers { get; set; } = false;
        public bool? WithCampaigns { get; set; } = false;
        public bool? WithMembers { get; set; } = false;
    }
}