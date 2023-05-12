using AutoMapper;
using Boek.Infrastructure.Requests.Addresses;
using Boek.Infrastructure.ViewModels.Addresses;

namespace Boek.Infrastructure.Mappings
{
    public static class AddressModule
    {
        public static void ConfigAddressModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<AddressRequestModel, AddressViewModel>()
            .ReverseMap();
        }
    }
}
