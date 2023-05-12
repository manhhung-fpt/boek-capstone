using AutoMapper;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.Users.Customers;
using Boek.Infrastructure.ViewModels.Campaigns.Mobile.Customers;
using Boek.Infrastructure.ViewModels.Users.Customers;

namespace Boek.Infrastructure.Mappings
{
    public static class CustomerModule
    {
        public static void ConfigCustomerModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<UpdateCustomerRequestModel, Customer>()
                .ForMember(src => src.IdNavigation, dst => dst.MapFrom(c => c.User))
                .ReverseMap();
            mc.CreateMap<Customer, CustomerUserViewModel>()
                .ForMember(src => src.User, dst => dst.MapFrom(c => c.IdNavigation))
                .ReverseMap();
            mc.CreateMap<CustomerViewModel, Customer>()
                .ForMember(src => src.IdNavigation, dst => dst.MapFrom(c => c.User))
                .ForMember(src => src.Level, dst => dst.MapFrom(c => c.Level))
                .ReverseMap();
            mc.CreateMap<Customer, CustomerLevelViewModel>()
                .ForMember(src => src.Level, dst => dst.MapFrom(c => c.Level))
                .ReverseMap();
            mc.CreateMap<Customer, CreateCustomerRequestModel>()
            .ForMember(src => src.Address, o => o.Ignore())
            .ForMember(src => src.GroupIds, o => o.Ignore())
            .ReverseMap();
            mc.CreateMap<BasicCustomerViewModel, Customer>().ReverseMap();
            mc.CreateMap<CustomerMobileViewModel, Customer>()
            .ForMember(src => src.IdNavigation, dst => dst.MapFrom(c => c.User))
            .ForMember(src => src.CustomerGroups, o => o.Ignore())
            .ForMember(src => src.CustomerOrganizations, o => o.Ignore())
            .ReverseMap();
            mc.CreateMap<Customer, CustomerOrdersViewModel>()
            .ForMember(dst => dst.User, src => src.MapFrom(c => c.IdNavigation))
            .ForMember(dst => dst.Level, src => src.MapFrom(c => c.Level))
            .ForMember(dst => dst.Orders, src => src.MapFrom(c => c.Orders))
            .ReverseMap();
        }
    }
}
