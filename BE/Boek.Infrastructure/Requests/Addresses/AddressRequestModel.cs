namespace Boek.Infrastructure.Requests.Addresses
{
    public class AddressRequestModel
    {
        public string Detail { get; set; }
        public int? ProvinceCode { get; set; }
        public int? DistrictCode { get; set; }
        public int? WardCode { get; set; }

        public bool IsEmptyAllFields()
        => String.IsNullOrEmpty(Detail) && !ProvinceCode.HasValue && !DistrictCode.HasValue && !WardCode.HasValue;
    }
}