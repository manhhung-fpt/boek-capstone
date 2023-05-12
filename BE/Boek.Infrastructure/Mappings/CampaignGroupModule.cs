using AutoMapper;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.CampaignGroups;
using Boek.Infrastructure.ViewModels.CampaignGroups;

namespace Boek.Infrastructure.Mappings
{
    public static class CampaignGroupModule
    {
        public static void ConfigCampaignGroupModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<CampaignGroup, BasicCampaignGroupViewModel>().ReverseMap();
            mc.CreateMap<CampaignGroup, CampaignGroupViewModel>()
                .ForMember(dst => dst.Campaign, src => src.MapFrom(co => co.Campaign))
                .ForMember(dst => dst.Group, src => src.MapFrom(co => co.Group))
                .ReverseMap();
            mc.CreateMap<CampaignGroup, CampaignGroupsViewModel>()
                .ForMember(dst => dst.Group, src => src.MapFrom(co => co.Group))
                .ReverseMap();
            mc.CreateMap<CampaignGroup, CampaignGroupRequestModel>().ReverseMap();
            mc.CreateMap<CampaignGroupRequestModel, CampaignGroupViewModel>().ReverseMap();
            mc.CreateMap<CampaignGroup, CreateCampaignGroupRequestModel>().ReverseMap();
        }
    }
}
