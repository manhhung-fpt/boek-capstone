using AutoMapper;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.Users.Issuers;
using Boek.Infrastructure.ViewModels.Users.Issuers;

namespace Boek.Infrastructure.Mappings
{
    public static class IssuerModule
    {
        public static void ConfigIssuerModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<UpdateIssuerRequestModel, Issuer>()
                .ForMember(dst => dst.IdNavigation, src => src.MapFrom(c => c.User))
                .ReverseMap();
            mc.CreateMap<BasicIssuerViewModel, Issuer>().ReverseMap();
            mc.CreateMap<IssuerViewModel, Issuer>()
                .ForMember(dst => dst.IdNavigation, src => src.MapFrom(c => c.User))
                .ReverseMap();
        }
    }
}
