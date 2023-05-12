using AutoMapper;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.CustomerOrganizations;
using Boek.Infrastructure.ViewModels.CustomerOrganizations;

namespace Boek.Infrastructure.Mappings
{
    public static class CustomerOrganizationModule
    {
        public static void ConfigCustomerOrganizationModule(
            this IMapperConfigurationExpression mc
        )
        {
            mc.CreateMap<CustomerOrganization, CustomerOrganizationViewModel>()
                .ForMember(dst => dst.Customer, src => src.MapFrom(co => co.Customer))
                .ForMember(dst => dst.Organization, src => src.MapFrom(co => co.Organization))
                .ReverseMap();
            mc.CreateMap<CustomerOrganization, OwnedCustomerOrganizationViewModel>()
                .ForMember(dst => dst.Customer, src => src.MapFrom(co => co.Customer))
                .ReverseMap();
            mc.CreateMap<CustomerOrganization, CustomerOrganizationRequestModel>()
                .ReverseMap();
            mc.CreateMap<CustomerOrganizationRequestModel, CustomerOrganizationViewModel>()
                .ReverseMap();
            mc.CreateMap<CustomerOrganization, CreateCustomerOrganizationRequestModel>()
                .ReverseMap();
        }
    }
}
