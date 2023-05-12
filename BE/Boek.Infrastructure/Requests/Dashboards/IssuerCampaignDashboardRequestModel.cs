using System.Text.Json.Serialization;

namespace Boek.Infrastructure.Requests.Dashboards
{
    public class IssuerCampaignDashboardRequestModel
    {
        [JsonIgnore]
        public int CampaignId { get; set; }
        public DashboardRequestModel DashboardRequestModel { get; set; }
        public bool? IsAllTheTime { get; set; }

        public void SetDefaultAllTheTime(bool result = false)
        {
            if (!IsAllTheTime.HasValue)
                IsAllTheTime = false;
        }
    }
}