using Boek.Core.Data;
using Boek.Core.Entities;
using Boek.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Boek.Repository.Repositories
{
    public class CampaignStaffRepository : GenericRepository<CampaignStaff>, ICampaignStaffRepository
    {
        public CampaignStaffRepository(BoekCapstoneContext context) : base(context)
        {
        }

        public List<Campaign> GetCampaigns(Guid? StaffId)
        {
            var campaigns = new List<Campaign>();
            var list =
                dbSet
                    .Where(cs => cs.StaffId.Equals(StaffId))
                    .Include(co => co.Campaign);
            if (list != null)
                campaigns = list.Select(co => co.Campaign).ToList();
            return campaigns;
        }

        public List<User> GetStaffs(int? CampaignId)
        {
            var staffs = new List<User>();
            var list =
                dbSet
                    .Where(cs => cs.CampaignId.Equals(CampaignId))
                    .Include(co => co.Staff);
            if (list != null)
                staffs = list.Select(co => co.Staff).ToList();
            return staffs;
        }
    }
}
