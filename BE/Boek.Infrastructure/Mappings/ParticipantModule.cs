using AutoMapper;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.Participants;
using Boek.Infrastructure.Requests.Participants.Mobile;
using Boek.Infrastructure.ViewModels.Participants;

namespace Boek.Infrastructure.Mappings
{
    public static class ParticipantModule
    {
        public static void ConfigParticipantModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Participant, ParticipantRequestModel>().ReverseMap();
            mc.CreateMap<Participant, BasicParticipantViewModel>().ReverseMap();
            mc.CreateMap<Participant, CampaignParticipationsViewModel>()
            .ForMember(dst => dst.Issuer, src => src.MapFrom(p => p.Issuer))
            .ReverseMap();
            mc.CreateMap<ParticipantViewModel, Participant>()
            .ForMember(dst => dst.Campaign, src => src.MapFrom(p => p.Campaign))
            .ForMember(dst => dst.Issuer, src => src.MapFrom(p => p.Issuer))
            .ReverseMap();
            mc.CreateMap<ParticipantRequestModel, ParticipantViewModel>().ReverseMap();
            mc.CreateMap<Participant, ApplyParticipantRequestModel>().ReverseMap();
            mc.CreateMap<Participant, InviteParticipantRequestModel>().ReverseMap();
            mc.CreateMap<Participant, UpdateParticipantRequestModel>().ReverseMap()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcParticipant) => srcParticipant != null));
            mc.CreateMap<ParticipantMobileRequestModel, ParticipantMobileFilterRequestModel>()
            .ForMember(dst => dst.IssuerIds, src => src.MapFrom(p => p.IssuerIds))
            .ReverseMap();
        }
    }
}