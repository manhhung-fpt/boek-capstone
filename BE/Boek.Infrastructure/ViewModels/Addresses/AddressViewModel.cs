using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.ViewModels.Addresses
{
    public class AddressViewModel
    {
        [String]
        public string Detail { get; set; }
        [Int]
        public int? ProvinceCode { get; set; }
        [Int]
        public int? DistrictCode { get; set; }
        [Int]
        public int? WardCode { get; set; }
    }
}