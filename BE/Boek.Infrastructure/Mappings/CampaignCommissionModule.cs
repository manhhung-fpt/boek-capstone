using AutoMapper;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.CampaignCommissions;
using Boek.Infrastructure.ViewModels.CampaignCommissions;

namespace Boek.Infrastructure.Mappings
{
    public static class CampaignCommissionModule
    {
        public static void ConfigCampaignCommissionModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<CampaignCommission, CampaignCommissionViewModel>() .ReverseMap()
                .ForMember(src => src.Campaign, dst => dst.MapFrom(cc => cc.Campaign))
                .ForMember(src => src.Genre, dst => dst.MapFrom(cc => cc.Genre));
            mc.CreateMap<CampaignCommission, CampaignCommissionsViewModel>().ReverseMap()
                .ForMember(src => src.Genre, dst => dst.MapFrom(cc => cc.Genre));
            mc.CreateMap<CampaignCommissionViewModel, CampaignCommissionRequestModel>().ReverseMap();
            mc.CreateMap<CampaignCommission, CampaignCommissionsRequestModel>().ReverseMap();
            mc.CreateMap <CampaignCommission, CreateCampaignCommissionRequestModel>() .ReverseMap();
            mc.CreateMap <CampaignCommission, UpdateCampaignCommissionRequestModel>() .ReverseMap();
        }
    }
}
