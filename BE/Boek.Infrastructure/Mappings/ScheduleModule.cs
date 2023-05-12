using AutoMapper;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.Schedules;
using Boek.Infrastructure.ViewModels.Schedules;

namespace Boek.Infrastructure.Mappings
{
    public static class ScheduleModule
    {
        public static void ConfigScheduleModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Schedule, SchedulesViewModel>()
            .ForMember(dst => dst.AddressViewModel, o => o.Ignore())
            .ForMember(dst => dst.StatusName, o => o.Ignore())
            .ReverseMap();
            mc.CreateMap<Schedule, ScheduleRequestModel>().ReverseMap();
            mc.CreateMap<Schedule, CreateSchedulesRequestModel>()
            .ForMember(dst => dst.AddressRequest, o => o.Ignore())
            .ReverseMap();
            mc.CreateMap<SchedulesViewModel, CreateSchedulesRequestModel>()
            .ForMember(dst => dst.AddressRequest, src => src.MapFrom(svm => svm.AddressViewModel))
            .ReverseMap();
            mc.CreateMap<ScheduleRequestModel, SchedulesViewModel>()
            .ForMember(dst => dst.AddressViewModel, o => o.Ignore())
            .ReverseMap();
        }
    }
}
