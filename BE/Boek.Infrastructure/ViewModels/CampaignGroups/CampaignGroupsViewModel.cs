using Boek.Infrastructure.ViewModels.Groups;

namespace Boek.Infrastructure.ViewModels.CampaignGroups
{
    public class CampaignGroupsViewModel
    {
        public int Id { get; set; }

        public int? CampaignId { get; set; }

        public int? GroupId { get; set; }

        public BasicGroupViewModel Group { get; set; }
    }
}
