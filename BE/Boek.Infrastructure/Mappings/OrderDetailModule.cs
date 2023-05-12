using AutoMapper;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.OrderDetails;
using Boek.Infrastructure.Requests.Orders;
using Boek.Infrastructure.ViewModels.OrderDetails;
using Boek.Infrastructure.ViewModels.OrderDetails.Calculation;
using Boek.Infrastructure.ViewModels.Orders;

namespace Boek.Infrastructure.Mappings
{
    public static class OrderDetailModule
    {
        public static void ConfigOrderDetailModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<OrderDetail, OrderDetailsViewModel>()
            .ForMember(src => src.BookProduct, dst => dst.MapFrom(od => od.BookProduct))
            .ReverseMap();
            mc.CreateMap<OrderDetail, OrderDetailsCalculationViewModel>()
            .ReverseMap();
            mc.CreateMap<OrderDetailsViewModel, OrderDetailsCalculationViewModel>()
            .ReverseMap();
            mc.CreateMap<OrderDetail, OrderDetailRequestModel>()
            .ForMember(src => src.BookProduct, dst => dst.MapFrom(od => od.BookProduct))
            .ReverseMap();
            mc.CreateMap<OrderDetail, OrderDetailsRequestModel>()
            .ForMember(src => src.BookProduct, dst => dst.MapFrom(od => od.BookProduct))
            .ReverseMap();
            mc.CreateMap<OrderDetailRequestModel, OrderDetailsRequestModel>()
            .ForMember(src => src.BookProduct, dst => dst.MapFrom(od => od.BookProduct))
            .ReverseMap();
            mc.CreateMap<OrderDetailRequestModel, OrderDetailsViewModel>()
            .ForMember(src => src.BookProduct, dst => dst.MapFrom(od => od.BookProduct))
            .ReverseMap();
            mc.CreateMap<OrderDetailsRequestModel, OrderDetailsViewModel>()
            .ForMember(src => src.BookProduct, dst => dst.MapFrom(od => od.BookProduct))
            .ReverseMap();
            mc.CreateMap<OrderDetail, BasicOrderDetailsViewModel>()
            .ReverseMap();
            mc.CreateMap<OrderDetail, CreateOrderDetailsRequestModel>()
            .ReverseMap();
            mc.CreateMap<CreateOrderDetailsRequestModel, OrderDetailsViewModel>()
            .ReverseMap();
            mc.CreateMap<OrderOrderDetailCreateRequestModel, OrderDetail>()
            .ReverseMap();

            #region ZaloPay
            mc.CreateMap<OrderDetailsViewModel, ZaloPayItemParams>()
            .ForMember(dst => dst.ItemId, src => src.MapFrom(od => od.BookProductId))
            .ForPath(dst => dst.ItemName, src => src.MapFrom(od => od.BookProduct.Title))
            .ForMember(dst => dst.ItemPrice, src => src.MapFrom(od => od.Price))
            .ForMember(dst => dst.ItemQuantity, src => src.MapFrom(od => od.Quantity))
            .ReverseMap();
            #endregion
        }
    }
}