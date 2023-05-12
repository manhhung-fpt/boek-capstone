using AutoMapper;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.BookProductItems;
using Boek.Infrastructure.ViewModels.BookProductItems;

namespace Boek.Infrastructure.Mappings
{
    public static class BookProductItemModule
    {
        public static void ConfigBookProductItemModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<BookProductItem, BookProductItemViewModel>()
            .ForMember(dst => dst.Book, src => src.MapFrom(bpi => bpi.Book))
            .ReverseMap();
            mc.CreateMap<BookProductItem, BasicBookProductItemViewModel>()
            .ForMember(dest => dest.Book, src => src.MapFrom(bpi => bpi.Book))
            .ReverseMap();
            mc.CreateMap<BasicBookProductItemViewModel, BookProductItemRequestModel>().ReverseMap();
            mc.CreateMap<BookProductItem, BookProductItemRequestModel>().ReverseMap();
            mc.CreateMap<BookProductItemViewModel, BookProductItemRequestModel>().ReverseMap();
            mc.CreateMap<BookProductItem, CreateBookProductItemRequestModel>().ReverseMap();
            mc.CreateMap<BookProductItem, UpdateBookProductItemRequestModel>().ReverseMap();
        }
    }
}