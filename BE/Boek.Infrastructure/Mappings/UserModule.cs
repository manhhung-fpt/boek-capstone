using AutoMapper;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.Users;
using Boek.Infrastructure.ViewModels.Users;

namespace Boek.Infrastructure.Mappings
{
    public static class UserModule
    {
        public static void ConfigUserModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<User, CreateUserRequestModel>().ReverseMap();
            mc.CreateMap<UpdateUserRequestModel, User>().ReverseMap()
            .ForMember(src => src.AddressRequest, o => o.Ignore())
            .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember) => srcMember != null));
            mc.CreateMap<User, UserViewModel>()
            .ForMember(src => src.AddressViewModel, o => o.Ignore())
            .ReverseMap();
            mc.CreateMap<User, UserRequestModel>()
            .ForMember(dst => dst.WithAddressDetail, o => o.Ignore())
            .ReverseMap();
            mc.CreateMap<UserRequestModel, MultiUserViewModel>()
            .ForMember(src => src.AddressViewModel, o => o.Ignore())
            .ReverseMap();
            mc.CreateMap<User, MultiUserViewModel>()
            .ForMember(src => src.Customer, dst => dst.MapFrom(u => u.Customer))
            .ForMember(src => src.Issuer, dst => dst.MapFrom(u => u.Issuer))
            .ForMember(src => src.AddressViewModel, o => o.Ignore())
            .ReverseMap();
            mc.CreateMap<UserRequestModel, UserViewModel>()
            .ForMember(src => src.AddressViewModel, o => o.Ignore())
            .ReverseMap();
            mc.CreateMap<UserViewModel, MultiUserViewModel>()
            .ForMember(src => src.AddressViewModel, dst => dst.MapFrom(u => u.AddressViewModel))
            .ReverseMap();
        }
    }
}
