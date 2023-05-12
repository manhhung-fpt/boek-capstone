using AutoMapper;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.Books.BookItems;
using Boek.Infrastructure.ViewModels.Books.BookItems;

namespace Boek.Infrastructure.Mappings
{
    public static class BookItemModule
    {
        public static void ConfigBookItemModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<CreateBookItemRequestModel, BookItem>().ReverseMap();
            mc.CreateMap<UpdateBookItemRequestModel, BookItem>().ReverseMap();
            mc.CreateMap<BookItem, ParentBookItemViewModel>()
                .ForMember(src => src.Book, dst => dst.MapFrom(bi => bi.Book))
                .ReverseMap();
        }
    }
}
