using AutoMapper;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.Organizations;
using Boek.Infrastructure.Requests.Schedules;
using Boek.Infrastructure.ViewModels.Organizations;
using Boek.Infrastructure.ViewModels.Organizations.Mobile;

namespace Boek.Infrastructure.Mappings
{
    public static class OrganizationModule
    {
        public static void ConfigOrganizationModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Organization, BasicOrganizationViewModel>()
            .ReverseMap();
            mc.CreateMap<BasicOrganizationViewModel, UpdateOrganizationRequestModel>()
            .ForMember(dst => dst.OrganizationMembers, o => o.Ignore())
            .ReverseMap();
            mc.CreateMap<Organization, OrganizationViewModel>().ReverseMap();
            mc.CreateMap<OrganizationRequestModel, OrganizationViewModel>()
            .ReverseMap();
            mc.CreateMap<Organization, CreateOrganizationRequestModel>()
            .ForMember(dst => dst.OrganizationMembers, src => src.MapFrom(o => o.OrganizationMembers))
            .ReverseMap();
            mc.CreateMap<Organization, UpdateOrganizationRequestModel>()
            .ForMember(dst => dst.OrganizationMembers, src => src.MapFrom(o => o.OrganizationMembers))
            .ReverseMap();
            mc.CreateMap<Organization, OrganizationRequestModel>().ReverseMap();

            #region Mobile
            mc.CreateMap<Organization, OrganizationsMobileViewModel>()
            .ForMember(dst => dst.Schedules, o => o.Ignore())
            .ReverseMap();
            mc.CreateMap<BasicOrganizationViewModel, OrganizationsMobileViewModel>()
           .ForMember(dst => dst.Schedules, o => o.Ignore())
           .ReverseMap();
            #endregion
        }
    }
}
