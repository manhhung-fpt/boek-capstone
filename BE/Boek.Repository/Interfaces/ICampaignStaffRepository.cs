using Boek.Core.Entities;

namespace Boek.Repository.Interfaces
{
    public interface ICampaignStaffRepository : IGenericRepository<CampaignStaff>
    {
        List<User> GetStaffs(int? CampaignId);
        List<Campaign> GetCampaigns(Guid? StaffId);
    }
}
