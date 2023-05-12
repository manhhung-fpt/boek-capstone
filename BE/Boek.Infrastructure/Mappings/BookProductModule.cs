using AutoMapper;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.BookProducts;
using Boek.Infrastructure.Requests.BookProducts.BookComboProducts;
using Boek.Infrastructure.Requests.BookProducts.BookSeriesProducts;
using Boek.Infrastructure.Requests.BookProducts.Mobile;
using Boek.Infrastructure.ViewModels.BookProducts;
using Boek.Infrastructure.ViewModels.BookProducts.Mobile;

namespace Boek.Infrastructure.Mappings
{
    public static class BookProductModule
    {
        public static void ConfigBookProductModule(this IMapperConfigurationExpression mc)
        {
            #region Book Products
            mc.CreateMap<BookProduct, BookProductRequestModel>().ReverseMap();
            mc.CreateMap<BookProduct, BookProductOrderDetailRequestModel>().ReverseMap();
            mc.CreateMap<BookProduct, IssuerComboBookProductRequestModel>().ReverseMap();
            mc.CreateMap<BookProduct, BasicBookProductViewModel>().ReverseMap();
            mc.CreateMap<BookProduct, BookProductViewModel>()
            .ForMember(dst => dst.Book, src => src.MapFrom(bp => bp.Book))
            .ForMember(dst => dst.Campaign, src => src.MapFrom(bp => bp.Campaign))
            .ForMember(dst => dst.Genre, src => src.MapFrom(bp => bp.Genre))
            .ForMember(dst => dst.Issuer, src => src.MapFrom(bp => bp.Issuer))
            .ForMember(dst => dst.BookProductItems, src => src.MapFrom(bp => bp.BookProductItems))
            .ReverseMap();
            mc.CreateMap<BookProduct, BookProductOrderDetailsViewModel>()
            .ForMember(dst => dst.OrderDetails, src => src.MapFrom(bp => bp.OrderDetails))
            .ReverseMap();
            mc.CreateMap<OrderBookProductsViewModel, BookProductOrderDetailsViewModel>()
            .ReverseMap();
            mc.CreateMap<OrderBookProductsViewModel, BookProductOrderDetailRequestModel>()
            .ReverseMap();
            mc.CreateMap<BookProductViewModel, BookProductOrderDetailsViewModel>()
            .ReverseMap();
            mc.CreateMap<BookProductViewModel, BookProductRequestModel>().ReverseMap();
            mc.CreateMap<BookProductViewModel, BookProductOrderDetailRequestModel>().ReverseMap();
            mc.CreateMap<BookProductViewModel, IssuerComboBookProductRequestModel>().ReverseMap();
            mc.CreateMap<BasicBookProductViewModel, BookProductRequestModel>().ReverseMap();
            mc.CreateMap<BookProductRequestModel, BookProductOrderDetailRequestModel>().ReverseMap();
            mc.CreateMap<BasicBookProductViewModel, BookProductOrderDetailRequestModel>().ReverseMap();
            mc.CreateMap<BookProduct, BookProductsViewModel>()
            .ForMember(dst => dst.Book, src => src.MapFrom(bp => bp.Book))
            .ForMember(dst => dst.Issuer, src => src.MapFrom(bp => bp.Issuer))
            .ForMember(dst => dst.BookProductItems, src => src.MapFrom(bp => bp.BookProductItems))
            .ReverseMap();
            mc.CreateMap<BookProduct, OrderBookProductsViewModel>()
            .ForMember(dst => dst.Book, src => src.MapFrom(bp => bp.Book))
            .ForMember(dst => dst.Issuer, src => src.MapFrom(bp => bp.Issuer))
            .ForMember(dst => dst.BookProductItems, src => src.MapFrom(bp => bp.BookProductItems))
            .ReverseMap();
            mc.CreateMap<BasicBookProductViewModel, OrderBookProductsViewModel>()
            .ReverseMap();
            mc.CreateMap<BookProduct, CreateBookProductRequestModel>().ReverseMap();
            mc.CreateMap<BookProduct, UpdateBookProductRequestModel>().ReverseMap();
            mc.CreateMap<BookProduct, CheckBookProductRequestModel>().ReverseMap();
            mc.CreateMap<BookProduct, UpdateBookProductStartedCampaignRequestModel>().ReverseMap();
            #endregion

            #region Book Series Products
            mc.CreateMap<BookProduct, CreateBookSeriesProductRequestModel>()
            .ForMember(dst => dst.BookProductItems, src => src.MapFrom(bp => bp.BookProductItems))
            .ReverseMap();
            mc.CreateMap<BookProduct, BasicCreateBookSeriesProductRequestModel>()
            .ReverseMap();
            mc.CreateMap<CreateBookSeriesProductRequestModel, BasicCreateBookSeriesProductRequestModel>()
            .ReverseMap();
            mc.CreateMap<BookProduct, UpdateBookSeriesProductRequestModel>()
            .ForMember(dst => dst.BookProductItems, src => src.MapFrom(bp => bp.BookProductItems))
            .ReverseMap();
            mc.CreateMap<BookProduct, BasicUpdateBookSeriesProductRequestModel>()
            .ReverseMap();
            mc.CreateMap<UpdateBookSeriesProductRequestModel, BasicUpdateBookSeriesProductRequestModel>()
            .ReverseMap();
            #endregion

            #region Book Combo Products
            mc.CreateMap<BookProduct, CreateBookComboProductRequestModel>()
            .ForMember(dst => dst.BookProductItems, src => src.MapFrom(bp => bp.BookProductItems))
            .ReverseMap();
            mc.CreateMap<BookProduct, UpdateBookComboProductRequestModel>()
            .ForMember(dst => dst.BookProductItems, src => src.MapFrom(bp => bp.BookProductItems))
            .ReverseMap();
            #endregion

            #region Mobile
            mc.CreateMap<BookProduct, MobileBookProductsViewModel>()
            .ForMember(dst => dst.Campaign, src => src.MapFrom(bp => bp.Campaign))
            .ForMember(dst => dst.Issuer, src => src.MapFrom(bp => bp.Issuer))
            .ForMember(dst => dst.Book, src => src.MapFrom(bp => bp.Book))
            .ForMember(dst => dst.BookProductItems, src => src.MapFrom(bp => bp.BookProductItems))
            .ReverseMap();
            mc.CreateMap<BookProduct, MobileBookProductViewModel>()
            .ForMember(dst => dst.Campaign, src => src.MapFrom(bp => bp.Campaign))
            .ForMember(dst => dst.Issuer, src => src.MapFrom(bp => bp.Issuer))
            .ForMember(dst => dst.Book, src => src.MapFrom(bp => bp.Book))
            .ForMember(dst => dst.BookProductItems, src => src.MapFrom(bp => bp.BookProductItems))
            .ReverseMap();
            mc.CreateMap<OtherMobileBookProductsViewModel, BookProduct>()
            .ForPath(src => src.Campaign.Name, dst => dst.MapFrom(ombp => ombp.CampaignName))
            .ReverseMap();
            mc.CreateMap<BasicBookProductMobileRequestModel, BookProductMobileRequestModel>()
            .ForPath(src => src.Book.GenreIds, dst => dst.MapFrom(basic => basic.GenreIds))
            .ForMember(src => src.hierarchicalBook, o => o.Ignore())
            .ForMember(src => src.unhierarchicalBook, o => o.Ignore())
            .ReverseMap();
            #endregion
        }
    }
}