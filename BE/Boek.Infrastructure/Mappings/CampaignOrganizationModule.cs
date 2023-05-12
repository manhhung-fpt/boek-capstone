using AutoMapper;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.CampaignOrganizations;
using Boek.Infrastructure.ViewModels.CampaignOrganizations;

namespace Boek.Infrastructure.Mappings
{
    public static class CampaignOrganizationModule
    {
        public static void ConfigCampaignOrganizationModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<CampaignOrganization, BasicCampaignOrganizationViewModel>().ReverseMap();
            mc.CreateMap<CampaignOrganization, CampaignOrganizationViewModel>()
            .ForMember(dst => dst.Campaign, src => src.MapFrom(co => co.Campaign))
            .ForMember(dst => dst.Organization, src => src.MapFrom(co => co.Organization))
            .ForMember(dst => dst.Schedules, src => src.MapFrom(co => co.Schedules))
            .ReverseMap();
            mc.CreateMap<CampaignOrganization, CampaignOrganizationsViewModel>()
            .ForMember(dst => dst.Organization, src => src.MapFrom(co => co.Organization))
            .ForMember(dst => dst.Schedules, src => src.MapFrom(co => co.Schedules))
            .ReverseMap();
            mc.CreateMap<CampaignOrganization, CampaignOrganizationRequestModel>().ReverseMap();
            mc.CreateMap<CampaignOrganizationRequestModel, CampaignOrganizationViewModel>().ReverseMap();
            mc.CreateMap<CampaignOrganization, CreateCampaignOrganizationRequestModel>()
            .ForMember(dst => dst.Schedules, src => src.MapFrom(co => co.Schedules))
            .ReverseMap();
            mc.CreateMap<CampaignOrganization, CreateCampaignOrganizationsRequestModel>()
            .ForMember(dst => dst.Schedules, src => src.MapFrom(co => co.Schedules))
            .ReverseMap();
        }
    }
}
