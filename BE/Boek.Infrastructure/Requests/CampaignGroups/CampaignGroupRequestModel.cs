using Boek.Infrastructure.Attributes;
namespace Boek.Infrastructure.Requests.CampaignGroups{
    public class CampaignGroupRequestModel{
        [Int]
        public int? Id { get; set; }
        [Int]
        public int? CampaignId { get; set; }
        [Int]
        public int? GroupId { get; set; }
    }
}