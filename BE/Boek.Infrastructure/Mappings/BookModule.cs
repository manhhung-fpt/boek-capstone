using AutoMapper;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.Books;
using Boek.Infrastructure.Requests.Books.BookSeries;
using Boek.Infrastructure.ViewModels.Books;
using Boek.Infrastructure.ViewModels.Books.Issuers;

namespace Boek.Infrastructure.Mappings
{
    public static class BookModule
    {
        public static void ConfigBookModule(this IMapperConfigurationExpression mc)
        {
            #region Books
            mc.CreateMap<Book, BookRequestModel>()
                .ForMember(dst => dst.BookSize, src => src.MapFrom(b => b.Size))
                .ForMember(dst => dst.BookPage, src => src.MapFrom(b => b.Page))
                .ReverseMap();
            mc.CreateMap<Book, BookViewModel>()
                .ForMember(dst => dst.Genre, src => src.MapFrom(b => b.Genre))
                .ForMember(dst => dst.Issuer, src => src.MapFrom(b => b.Issuer))
                .ForMember(dst => dst.Publisher, src => src.MapFrom(b => b.Publisher))
                .ForMember(dst => dst.BookAuthors, src => src.MapFrom(b => b.BookAuthors))
                .ForMember(dst => dst.BookItems, src => src.MapFrom(b => b.BookItemParentBooks))
                .ReverseMap();
            mc.CreateMap<Book, CreateBookRequestModel>().ReverseMap();
            mc.CreateMap<Book, UpdateBookRequestModel>().ReverseMap();
            mc.CreateMap<BookRequestModel, BookViewModel>()
                .ForMember(dst => dst.Size, src => src.MapFrom(b => b.BookSize))
                .ForMember(dst => dst.Page, src => src.MapFrom(b => b.BookPage))
                .ReverseMap();
            mc.CreateMap<BasicBookViewModel, Book>().ReverseMap();
            mc.CreateMap<Book, IssuerBookViewModel>()
            .ForMember(dst => dst.Genre, src => src.MapFrom(b => b.Genre))
                .ForMember(dst => dst.Issuer, src => src.MapFrom(b => b.Issuer))
                .ForMember(dst => dst.Publisher, src => src.MapFrom(b => b.Publisher))
                .ForMember(dst => dst.BookAuthors, src => src.MapFrom(b => b.BookAuthors))
                .ForMember(dst => dst.BookItems, src => src.MapFrom(b => b.BookItemParentBooks))
            .ReverseMap();

            #endregion

            #region Book Series
            mc.CreateMap<Book, CreateBookSeriesRequestModel>()
                .ForMember(dst => dst.createBookItems, src => src.MapFrom(b => b.BookItemBooks))
                .ReverseMap();
            mc.CreateMap<Book, UpdateBookSeriesRequestModel>()
                .ForMember(dst => dst.updateBookItems, src => src.MapFrom(b => b.BookItemBooks))
                .ReverseMap();
            #endregion

            #region Book Product
            mc.CreateMap<Book, BookProductDetailViewModel>()
               .ForMember(dst => dst.Genre, src => src.MapFrom(b => b.Genre))
               .ForMember(dst => dst.Issuer, src => src.MapFrom(b => b.Issuer))
               .ForMember(dst => dst.Publisher, src => src.MapFrom(b => b.Publisher))
               .ForMember(dst => dst.BookAuthors, src => src.MapFrom(b => b.BookAuthors))
               .ReverseMap();
            #endregion
        }
    }
}
