using AutoMapper;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.CustomerGroups;
using Boek.Infrastructure.ViewModels.CustomerGroups;

namespace Boek.Infrastructure.Mappings
{
    public static class CustomerGroupModule
    {
        public static void ConfigCustomerGroupModule(
            this IMapperConfigurationExpression mc
        )
        {
            mc.CreateMap<CustomerGroup, CustomerGroupViewModel>()
              .ForMember(dst => dst.Customer, src => src.MapFrom(co => co.Customer))
              .ForMember(dst => dst.Group, src => src.MapFrom(co => co.Group))
              .ReverseMap();
            mc.CreateMap<CustomerGroup, OwnedCustomerGroupViewModel>()
                .ForMember(dst => dst.Customer, src => src.MapFrom(co => co.Customer))
                .ReverseMap();
            mc.CreateMap<CustomerGroup, CustomerGroupRequestModel>().ReverseMap();
            mc.CreateMap<CustomerGroupRequestModel, CustomerGroupViewModel>().ReverseMap();
            mc.CreateMap<CustomerGroup, CreateCustomerGroupRequestModel>().ReverseMap();
        }
    }
}
