using AutoMapper;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.Levels;
using Boek.Infrastructure.ViewModels.Levels;

namespace Boek.Infrastructure.Mappings
{
    public static class LevelModule
    {
        public static void ConfigLevelModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<LevelViewModel, LevelRequestModel>().ReverseMap();
            mc.CreateMap<Level, LevelViewModel>()
              .ForMember(dst => dst.Customers, src => src.MapFrom(l => l.Customers))
              .ReverseMap();
            mc.CreateMap<LevelRequestModel, Level>().ReverseMap();
            mc.CreateMap<BasicLevelViewModel, Level>().ReverseMap();
            mc.CreateMap<BasicLevelViewModel, LevelRequestModel>().ReverseMap();
            mc.CreateMap<Level, CreateLevelRequestModel>().ReverseMap();
            mc.CreateMap<Level, UpdateLevelRequestModel>().ReverseMap()
              .ForAllMembers(opts => opts.Condition((src, dest, srcLevel) => srcLevel != null));
        }
    }
}
