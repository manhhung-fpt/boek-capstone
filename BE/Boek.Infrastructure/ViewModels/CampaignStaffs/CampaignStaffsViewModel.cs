using System.Text.Json.Serialization;
using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.ViewModels.Campaigns;
using Boek.Infrastructure.ViewModels.Users;

namespace Boek.Infrastructure.ViewModels.CampaignStaffs
{
    public class CampaignStaffsViewModel
    {
        public int Total { get; set; } = 0;
        public BasicCampaignViewModel Campaign { get; set; }
        public List<UserViewModel> Staffs { get; set; }
        public void GetTotal()
        {
            if (Staffs != null)
            {
                if (Staffs.Any())
                    Total = Staffs.Count();
            }
        }
    }
}