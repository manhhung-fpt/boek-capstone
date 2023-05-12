using AutoMapper;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.Genres;
using Boek.Infrastructure.ViewModels.Genres;

namespace Boek.Infrastructure.Mappings
{
    public static class GenreModule
    {
        public static void ConfigGenreModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Genre, GenreViewModel>().ReverseMap();
            mc.CreateMap<Genre, GenreRequestModel>().ReverseMap();
            mc.CreateMap<GenreViewModel, GenreRequestModel>().ReverseMap();
            mc.CreateMap<GenreBooksViewModel, GenreRequestModel>().ReverseMap();
            mc.CreateMap<Genre, CreateGenreRequestModel>().ReverseMap();
            mc.CreateMap<Genre, UpdateGenreRequestModel>().ReverseMap()
                .ForAllMembers(opts =>opts.Condition((src, dest, srcGenre) => srcGenre != null));
            mc.CreateMap<ChildGenreViewModel, Genre>()
                .ForMember(dst => dst.Books, src => src.MapFrom(g => g.Books))
                .ReverseMap();
            mc.CreateMap<GenreBooksViewModel, Genre>()
                .ForMember(dst => dst.Books, src => src.MapFrom(g => g.Books))
                .ReverseMap();
            mc.CreateMap<ParentGenreViewModel, Genre>()
                .ForMember(dst => dst.InverseParent, src => src.MapFrom(pg => pg.Genres))
                .ReverseMap();
        }
    }
}
