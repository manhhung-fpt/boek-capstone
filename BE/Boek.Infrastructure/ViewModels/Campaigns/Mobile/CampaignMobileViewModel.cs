using System.Text.Json.Serialization;
using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.ViewModels.Users.Issuers;
using Boek.Infrastructure.ViewModels.BookProducts.Mobile;
using Boek.Infrastructure.ViewModels.Levels;
using Boek.Infrastructure.ViewModels.Organizations.Mobile;
using Boek.Infrastructure.ViewModels.Groups;

namespace Boek.Infrastructure.ViewModels.Campaigns.Mobile
{
    public class CampaignMobileViewModel
    {
        [Int]
        public int? Id { get; set; }
        [Guid]
        public Guid? Code { get; set; }

        [String]
        public string Name { get; set; }

        [String]
        public string Description { get; set; }

        [String]
        public string ImageUrl { get; set; }

        [Byte]
        public byte? Format { get; set; }

        [Byte]
        public string Address { get; set; }

        [StartDate]
        public DateTime? StartDate { get; set; }
        [EndDate]
        public DateTime? EndDate { get; set; }
        [Boolean]
        public bool? IsRecurring { get; set; }
        [Byte]
        public byte? Status { get; set; }

        [DateRange]
        public DateTime? CreatedDate { get; set; }

        [DateRange]
        public DateTime? UpdatedDate { get; set; }

        [String]
        public string StatusName { get; set; }

        [String]
        public string FormatName { get; set; }

        [Sort, JsonIgnore]
        public string Sort { get; set; }
        //TO-DO: Change
        public List<OrganizationsMobileViewModel> Organizations { get; set; }
        public List<IssuerViewModel> Issuers { get; set; }
        public List<BasicGroupViewModel> Groups { get; set; }
        public List<BasicLevelViewModel> Levels { get; set; }
        [JsonIgnore]
        public List<MobileBookProductsViewModel> BookProducts { get; set; }
        public List<HierarchicalBookProductsViewModel> HierarchicalBookProducts { get; set; }
        public List<UnhierarchicalBookProductsViewModel> UnhierarchicalBookProducts { get; set; }
    }
}