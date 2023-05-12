using AutoMapper;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.CampaignStaffs;
using Boek.Infrastructure.ViewModels.CampaignStaffs;

namespace Boek.Infrastructure.Mappings
{
    public static class CampaignStaffModule
    {
        public static void ConfigCampaignStaffModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<CampaignStaff, CampaignStaffViewModel>()
                .ForMember(dst => dst.Campaign, src => src.MapFrom(cs => cs.Campaign))
                .ForMember(dst => dst.Staff, src => src.MapFrom(cs => cs.Staff))
                .ReverseMap();
            mc.CreateMap<CampaignStaff, CampaignStaffsViewModel>()
                .ForMember(dst => dst.Campaign, src => src.MapFrom(cs => cs.Campaign))
                .ReverseMap();
            mc.CreateMap<Campaign, CampaignStaffsViewModel>()
                .ForMember(dst => dst.Campaign, src => src.MapFrom(cs => cs))
                .ReverseMap();
            mc.CreateMap<CampaignStaff, StaffCampaignsViewModel>()
                .ForMember(dst => dst.Staff, src => src.MapFrom(cs => cs.Staff))
                .ReverseMap();
            mc.CreateMap<CampaignStaff, OrderCampaignStaffViewModel>()
                .ForMember(dst => dst.Staff, src => src.MapFrom(cs => cs.Staff))
                .ReverseMap();
            mc.CreateMap<CampaignStaff, CampaignStaffRequestModel>()
                .ForMember(dst => dst.Staff, src => src.MapFrom(cs => cs.Staff))
                .ReverseMap();
            mc.CreateMap<OrderCampaignStaffViewModel, CampaignStaffRequestModel>()
                .ForMember(dst => dst.Staff, src => src.MapFrom(cs => cs.Staff))
                .ReverseMap();
            mc.CreateMap<CampaignStaffViewModel, CampaignStaffRequestModel>()
                .ForMember(dst => dst.Staff, src => src.MapFrom(cs => cs.Staff))
                .ReverseMap();
            mc.CreateMap<CampaignStaff, BasicCampaignStaffViewModel>().ReverseMap();
            mc.CreateMap<CampaignStaff, CreateCampaignStaffRequestModel>().ReverseMap();
            mc.CreateMap<CampaignStaff, MobileCampaignStaffsViewModel>()
            .ForMember(src => src.Staff, dst => dst.MapFrom(cs => cs.Staff))
            .ReverseMap();
        }
    }
}
