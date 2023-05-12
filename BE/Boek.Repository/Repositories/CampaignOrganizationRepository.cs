using Boek.Core.Data;
using Boek.Core.Entities;
using Boek.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Boek.Repository.Repositories
{
    public class
    CampaignOrganizationRepository
    : GenericRepository<CampaignOrganization>, ICampaignOrganizationRepository
    {
        public CampaignOrganizationRepository(BoekCapstoneContext context) :
            base(context)
        {
        }

        public List<Campaign> GetCampaigns(int? OrganizationId)
        {
            var campaigns = new List<Campaign>();
            var list =
                dbSet
                    .Where(co => co.OrganizationId.Equals(OrganizationId))
                    .Include(co => co.Campaign);
            if (list != null)
                campaigns = list.Select(co => co.Campaign).ToList();
            return campaigns;
        }

        public List<Organization> GetOrganizations(int? CampaignId)
        {
            var organizations = new List<Organization>();
            var list = dbSet.Where(co => co.CampaignId.Equals(CampaignId))
                    .Include(co => co.Organization).ToList();
            if (list.Any())
                organizations = list.Select(co => co.Organization).ToList();
            return organizations;
        }
        public Dictionary<Organization, List<Schedule>> GetOrganizationsWithSchedules(int? CampaignId)
        {
            var organizations = new Dictionary<Organization, List<Schedule>>();
            var list = dbSet.Where(co => co.CampaignId.Equals(CampaignId))
                    .Include(co => co.Organization)
                    .Include(co => co.Schedules)
                    .ToList();
            if (list.Any())
            {
                list.Select(cos => new
                {
                    organization = cos.Organization,
                    schedules = cos.Schedules.ToList()
                }).ToList().ForEach(o => organizations.Add(o.organization, o.schedules));
            }
            return organizations;
        }
    }
}
