using AutoMapper;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.Campaigns;
using Boek.Infrastructure.Requests.Campaigns.Mobile;
using Boek.Infrastructure.ViewModels.Campaigns;
using Boek.Infrastructure.ViewModels.Campaigns.Mobile;
using Boek.Infrastructure.ViewModels.Dashboards.Revenue;

namespace Boek.Infrastructure.Mappings
{
    public static class CampaignModule
    {
        public static void ConfigCampaignModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Campaign, CampaignRequestModel>()
            .ForMember(dst => dst.WithAddressDetail, o => o.Ignore())
            .ReverseMap();
            mc.CreateMap<CampaignRequestModel, CampaignBookProductRequestModel>()
            .ReverseMap();
            mc.CreateMap<Campaign, CampaignBookProductRequestModel>()
            .ReverseMap();
            mc.CreateMap<CampaignBookProductRequestModel, CampaignViewModel>()
            .ForMember(dst => dst.AddressViewModel, o => o.Ignore())
            .ReverseMap();
            mc.CreateMap<Campaign, CampaignOrderRequestModel>()
            .ReverseMap();
            mc.CreateMap<CampaignRequestModel, CampaignViewModel>()
            .ForMember(dst => dst.AddressViewModel, o => o.Ignore())
            .ReverseMap();
            mc.CreateMap<Campaign, CampaignOrderViewModel>()
            .ForMember(dst => dst.Orders, src => src.MapFrom(c => c.Orders))
            .ForMember(dst => dst.Participants, src => src.MapFrom(c => c.Participants))
            .ReverseMap();
            mc.CreateMap<BasicCampaignViewModel, CampaignOrderViewModel>()
            .ReverseMap();
            mc.CreateMap<CampaignOrderViewModel, RevenueViewModel<BasicCampaignViewModel>>()
            .ForPath(dst => dst.Data, src => src.MapFrom(response => response))
            .ReverseMap();
            mc.CreateMap<Campaign, BasicCampaignViewModel>()
            .ForMember(dst => dst.AddressViewModel, o => o.Ignore())
            .ReverseMap();
            mc.CreateMap<Campaign, CampaignViewModel>()
            .ForMember(dst => dst.AddressViewModel, o => o.Ignore())
            .ForMember(dst => dst.CampaignCommissions, src => src.MapFrom(c => c.CampaignCommissions))
            .ForMember(dst => dst.CampaignOrganizations, src => src.MapFrom(c => c.CampaignOrganizations))
            .ForMember(dst => dst.CampaignGroups, src => src.MapFrom(c => c.CampaignGroups))
            .ForMember(dst => dst.Participants, src => src.MapFrom(c => c.Participants))
            .ReverseMap();
            mc.CreateMap<CampaignViewModel, CampaignOrderRequestModel>()
            .ReverseMap();
            mc.CreateMap<BasicCampaignViewModel, CampaignOrderRequestModel>()
            .ReverseMap();
            mc.CreateMap<Campaign, CampaignOrderRequestModel>()
            .ReverseMap();
            mc.CreateMap<Campaign, CreateOfflineCampaignRequestModel>()
            .ForMember(dst => dst.AddressRequest, o => o.Ignore())
            .ForMember(dst => dst.CampaignCommissions, src => src.MapFrom(c => c.CampaignCommissions))
            .ForMember(dst => dst.CampaignOrganizations, src => src.MapFrom(c => c.CampaignOrganizations))
            .ReverseMap();
            mc.CreateMap<Campaign, CreateOnlineCampaignRequestModel>()
            .ForMember(dst => dst.CampaignCommissions, src => src.MapFrom(c => c.CampaignCommissions))
            .ReverseMap();
            mc.CreateMap<Campaign, UpdateOfflineCampaignRequestModel>()
            .ForMember(dst => dst.AddressRequest, o => o.Ignore())
            .ForMember(dst => dst.CampaignCommissions, src => src.MapFrom(c => c.CampaignCommissions))
            .ForMember(dst => dst.CampaignOrganizations, src => src.MapFrom(c => c.CampaignOrganizations))
            .ReverseMap();
            mc.CreateMap<Campaign, UpdateOnlineCampaignRequestModel>()
            .ForMember(dst => dst.CampaignCommissions, src => src.MapFrom(c => c.CampaignCommissions))
            .ReverseMap();
            mc.CreateMap<Campaign, BasicUpdateCampaignRequestModel>()
            .ReverseMap();
            mc.CreateMap<BasicUpdateCampaignRequestModel, UpdateOfflineCampaignRequestModel>()
            .ReverseMap();
            mc.CreateMap<BasicUpdateCampaignRequestModel, UpdateOnlineCampaignRequestModel>()
            .ReverseMap();
            mc.CreateMap<UpdateCampaignRequestModel, Campaign>().ReverseMap();
            mc.CreateMap<Campaign, CampaignSchedulesViewModel>()
            .ForMember(dst => dst.OccurringProvinces, o => o.Ignore())
            .ForMember(dst => dst.Schedules, o => o.Ignore())
            .ReverseMap();
            mc.CreateMap<CampaignRequestModel, CampaignSchedulesViewModel>()
            .ForMember(dst => dst.OccurringProvinces, o => o.Ignore())
            .ForMember(dst => dst.Schedules, o => o.Ignore())
            .ReverseMap();
            mc.CreateMap<Campaign, CampaignParticipantsViewModel>()
            .ForMember(dst => dst.Participants, src => src.MapFrom(c => c.Participants))
            .ReverseMap();

            #region Mobile
            mc.CreateMap<Campaign, CampaignMobileViewModel>()
            .ReverseMap();
            mc.CreateMap<Campaign, CampaignsMobileViewModel>()
            .ForMember(dst => dst.CampaignCommissions, src => src.MapFrom(c => c.CampaignCommissions))
            .ForMember(dst => dst.CampaignOrganizations, src => src.MapFrom(c => c.CampaignOrganizations))
            .ForMember(dst => dst.CampaignGroups, src => src.MapFrom(c => c.CampaignGroups))
            .ForMember(dst => dst.Participants, src => src.MapFrom(c => c.Participants))
            .ForMember(dst => dst.CampaignLevels, src => src.MapFrom(c => c.CampaignLevels))
            .ForMember(dst => dst.BookProducts, o => o.Ignore())
            .ReverseMap();
            mc.CreateMap<CampaignsMobileViewModel, CampaignMobileViewModel>()
            .ForMember(dst => dst.BookProducts, src => src.MapFrom(c => c.BookProducts))
            .ReverseMap();
            mc.CreateMap<Campaign, StaffCampaignMobilesViewModel>()
            .ForMember(src => src.Issuers, o => o.Ignore())
            .ForMember(src => src.CampaignStaffs, o => o.Ignore())
            .ForMember(src => src.Schedules, o => o.Ignore())
            .ReverseMap();
            mc.CreateMap<Campaign, CampaignMobileRequestModel>()
            .ForMember(src => src.Formats, o => o.Ignore())
            .ForMember(src => src.Participants, o => o.Ignore())
            .ForMember(src => src.CampaignOrganizations, o => o.Ignore())
            .ReverseMap();
            mc.CreateMap<Campaign, CampaignMobileFilterViewModel>()
            .ForMember(src => src.Formats, o => o.Ignore())
            .ForMember(src => src.Participants, o => o.Ignore())
            .ForMember(src => src.CampaignOrganizations, o => o.Ignore())
            .ReverseMap();
            mc.CreateMap<CampaignMobileRequestModel, CampaignMobileFilterViewModel>()
            .ForMember(src => src.Participants, o => o.Ignore())
            .ForMember(src => src.CampaignGroups, dst => dst.MapFrom(cmrm => cmrm.CampaignGroups))
            .ForMember(src => src.CampaignLevels, dst => dst.MapFrom(cmrm => cmrm.CampaignLevels))
            .ForPath(src => src.CampaignOrganizations.OrganizationIds, dst => dst.MapFrom(cmrm => cmrm.CampaignOrganizations.OrganizationIds))
            .ForPath(src => src.CampaignOrganizations.Schedules.Address, dst => dst.MapFrom(cmrm => cmrm.Address))
            .ReverseMap();
            mc.CreateMap<StaffCampaignMobileRequestModel, StaffCampaignMobileFilterRequestModel>()
            .ForMember(src => src.CampaignOrganizations, dst => dst.MapFrom(scmrm => scmrm.Address))
            .ReverseMap();
            #endregion
        }
    }
}
