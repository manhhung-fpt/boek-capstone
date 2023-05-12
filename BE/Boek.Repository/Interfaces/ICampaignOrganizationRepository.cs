using Boek.Core.Entities;

namespace Boek.Repository.Interfaces
{
    public interface ICampaignOrganizationRepository : IGenericRepository<CampaignOrganization>
    {
        List<Campaign> GetCampaigns(int? OrganizationId);
        List<Organization> GetOrganizations(int? CampaignId);
        Dictionary<Organization, List<Schedule>> GetOrganizationsWithSchedules(int? CampaignId);
    }
}
