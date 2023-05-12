using AutoMapper;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.CampaignLevels;
using Boek.Infrastructure.ViewModels.CampaignLevels;

namespace Boek.Infrastructure.Mappings
{
    public static class CampaignLevelModule
    {
        public static void ConfigCampaignLevelModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<CampaignLevel, CampaignLevelViewModel>()
            .ForMember(src => src.Campaign, dst => dst.MapFrom(cl => cl.Campaign))
            .ForMember(src => src.Level, dst => dst.MapFrom(cl => cl.Level))
            .ReverseMap();
            mc.CreateMap<CampaignLevelViewModel, CampaignLevelRequestModel>().ReverseMap();
            mc.CreateMap<CampaignLevel, CampaignLevelsViewModel>()
            .ForMember(src => src.Level, dst => dst.MapFrom(cl => cl.Level))
            .ReverseMap();
            mc.CreateMap<CampaignLevelsViewModel, CampaignLevelRequestModel>().ReverseMap();
            mc.CreateMap<CampaignLevel, BasicCampaignLevelViewModel>()
            .ReverseMap();
            mc.CreateMap<BasicCampaignLevelViewModel, CampaignLevelRequestModel>().ReverseMap();
            mc.CreateMap<CampaignLevelRequestModel, CampaignLevel>().ReverseMap();
        }
    }
}
