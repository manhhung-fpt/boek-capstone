using AutoMapper;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.Orders;
using Boek.Infrastructure.Requests.Orders.Calculation;
using Boek.Infrastructure.Requests.Orders.Guest;
using Boek.Infrastructure.Requests.Orders.Update;
using Boek.Infrastructure.Requests.Orders.ZaloPay;
using Boek.Infrastructure.ViewModels.OrderDetails;
using Boek.Infrastructure.ViewModels.Orders;
using Boek.Infrastructure.ViewModels.Orders.Calculation;

namespace Boek.Infrastructure.Mappings
{
    public static class OrderMapping
    {
        public static void OrderMappingConfigure(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Order, OrderViewModel>()
            .ForMember(src => src.Campaign, dst => dst.MapFrom(o => o.Campaign))
            .ForMember(src => src.CampaignStaff, dst => dst.MapFrom(o => o.CampaignStaff))
            .ForMember(src => src.Customer, dst => dst.MapFrom(o => o.Customer))
            .ForMember(src => src.OrderDetails, dst => dst.MapFrom(o => o.OrderDetails))
            .ForMember(src => src.Total, o => o.Ignore())
            .ReverseMap();
            mc.CreateMap<Order, BasicOrderRequestModel>()
            .ReverseMap();
            mc.CreateMap<OrdersViewModel, OrderViewModel>()
            .ForMember(src => src.CampaignStaff, dst => dst.MapFrom(o => o.CampaignStaff))
            .ForMember(src => src.Customer, dst => dst.MapFrom(o => o.Customer))
            .ForMember(src => src.OrderDetails, dst => dst.MapFrom(o => o.OrderDetails))
            .ReverseMap();
            mc.CreateMap<OrderCalculationViewModel, Order>()
            .ForMember(src => src.OrderDetails, dst => dst.MapFrom(o => o.OrderDetails))
            .ReverseMap();
            mc.CreateMap<OrderCalculationViewModel, OrderViewModel>()
            .ForMember(src => src.OrderDetails, dst => dst.MapFrom(o => o.OrderDetails))
            .ReverseMap();
            mc.CreateMap<Order, OrdersViewModel>()
            .ForMember(src => src.CampaignStaff, dst => dst.MapFrom(o => o.CampaignStaff))
            .ForMember(src => src.Customer, dst => dst.MapFrom(o => o.Customer))
            .ForMember(src => src.OrderDetails, dst => dst.MapFrom(o => o.OrderDetails))
            .ForMember(src => src.Total, o => o.Ignore())
            .ReverseMap();
            mc.CreateMap<OrderFilterRequestModel, OrderViewModel>()
            .ForMember(src => src.CampaignStaff, dst => dst.MapFrom(o => o.CampaignStaff))
            .ForMember(src => src.Campaign, dst => dst.MapFrom(o => o.Campaign))
            .ForMember(src => src.Total, o => o.Ignore())
            .ForMember(src => src.OrderDetails, dst => dst.Ignore())
            .ReverseMap();
            mc.CreateMap<OrderRequestModel, OrderViewModel>()
            .ForMember(src => src.Total, o => o.Ignore())
            .ForMember(src => src.OrderDetails, o => o.Ignore())
            .AfterMap((src, dest, context) =>
            {
                if (src.OrderDetails != null)
                {
                    if (src.OrderDetails.BookProduct != null)
                    {
                        if (src.OrderDetails.BookProduct.IssuerId.HasValue)
                        {
                            var MapperObject = context.Mapper.Map<OrderDetailsViewModel>(src.OrderDetails);
                            if (dest.OrderDetails == null)
                                dest.OrderDetails = new List<OrderDetailsViewModel>();
                            dest.OrderDetails.Add(MapperObject);
                        }
                    }
                }
            })
            .ReverseMap();
            mc.CreateMap<Order, OrderRequestModel>()
            .ForMember(src => src.OrderDates, o => o.Ignore())
            .ForMember(src => src.OrderDetails, o => o.Ignore())
            .AfterMap((src, dest, context) =>
            {
                if (dest.OrderDetails != null)
                {
                    if (dest.OrderDetails.BookProduct != null)
                    {
                        if (dest.OrderDetails.BookProduct.IssuerId.HasValue)
                        {
                            var MapperObject = context.Mapper.Map<OrderDetail>(dest.OrderDetails);
                            if (dest.OrderDetails == null)
                                src.OrderDetails = new List<OrderDetail>();
                            src.OrderDetails.Add(MapperObject);
                        }
                    }
                }
            })
            .ReverseMap();
            mc.CreateMap<OrderFilterRequestModel, OrderRequestModel>()
            .ForMember(src => src.OrderDetails, dst => dst.MapFrom(orm => orm.OrderDetails))
            .ReverseMap();
            mc.CreateMap<Order, OrderFilterRequestModel>()
            .ForMember(src => src.CampaignStaff, dst => dst.MapFrom(o => o.CampaignStaff))
            .ForMember(src => src.OrderDetails, dst => dst.Ignore())
            .ReverseMap();
            mc.CreateMap<Order, OrderCreateModel>()
            .ReverseMap();
            mc.CreateMap<Order, OrderUpdateModel>()
            .ReverseMap();
            mc.CreateMap<Order, UpdateAvailableOrderRequestModel>()
            .ReverseMap();
            mc.CreateMap<Order, CreateShippingOrderRequestModel>()
            .ForMember(dst => dst.AddressRequest, o => o.Ignore())
            .ForMember(dst => dst.OrderDetails, src => src.MapFrom(o => o.OrderDetails))
            .ReverseMap();
            mc.CreateMap<Order, CreateCustomerPickUpOrderRequestModel>()
            .ForMember(dst => dst.OrderDetails, src => src.MapFrom(o => o.OrderDetails))
            .ReverseMap();
            mc.CreateMap<Order, CreateStaffPickUpOrderRequestModel>()
            .ForMember(dst => dst.OrderDetails, src => src.MapFrom(o => o.OrderDetails))
            .ReverseMap();
            mc.CreateMap<Order, CreateGuestPickUpOrderRequestModel>()
            .ForMember(dst => dst.OrderDetails, src => src.MapFrom(o => o.OrderDetails))
            .ReverseMap();
            mc.CreateMap<Order, CreateGuestShippingOrderRequestModel>()
            .ForMember(dst => dst.OrderDetails, src => src.MapFrom(o => o.OrderDetails))
            .ReverseMap();
            mc.CreateMap<Order, ShippingOrderCalculationRequestModel>()
            .ForMember(dst => dst.AddressRequest, o => o.Ignore())
            .ForMember(dst => dst.OrderDetails, src => src.MapFrom(o => o.OrderDetails))
            .ReverseMap();
            mc.CreateMap<Order, PickUpOrderCalculationRequestModel>()
            .ForMember(dst => dst.OrderDetails, src => src.MapFrom(o => o.OrderDetails))
            .ReverseMap();


            #region ZaloPay
            mc.CreateMap<Order, CreateZaloPayOrderRequestModel>()
            .ForMember(dst => dst.AddressRequest, o => o.Ignore())
            .ForMember(dst => dst.Description, o => o.Ignore())
            .ForMember(dst => dst.RedirectUrl, o => o.Ignore())
            .ForMember(dst => dst.OrderDetails, src => src.MapFrom(o => o.OrderDetails))
            .ReverseMap();
            mc.CreateMap<CreateShippingOrderRequestModel, CreateZaloPayOrderRequestModel>()
            .ForMember(dst => dst.Description, o => o.Ignore())
            .ForMember(dst => dst.RedirectUrl, o => o.Ignore())
            .ForMember(dst => dst.OrderDetails, src => src.MapFrom(o => o.OrderDetails))
            .ReverseMap();
            mc.CreateMap<CreateCustomerPickUpOrderRequestModel, CreateZaloPayOrderRequestModel>()
            .ForMember(dst => dst.AddressRequest, o => o.Ignore())
            .ForMember(dst => dst.Description, o => o.Ignore())
            .ForMember(dst => dst.RedirectUrl, o => o.Ignore())
            .ForMember(dst => dst.OrderDetails, src => src.MapFrom(o => o.OrderDetails))
            .ReverseMap();
            mc.CreateMap<CreateStaffPickUpOrderRequestModel, CreateZaloPayOrderRequestModel>()
            .ForMember(dst => dst.Description, o => o.Ignore())
            .ForMember(dst => dst.RedirectUrl, o => o.Ignore())
            .ForMember(dst => dst.AddressRequest, o => o.Ignore())
            .ForMember(dst => dst.OrderDetails, src => src.MapFrom(o => o.OrderDetails))
            .ReverseMap();
            mc.CreateMap<CreateGuestPickUpOrderRequestModel, CreateZaloPayOrderRequestModel>()
            .ForMember(dst => dst.Description, o => o.Ignore())
            .ForMember(dst => dst.RedirectUrl, o => o.Ignore())
            .ForMember(dst => dst.AddressRequest, o => o.Ignore())
            .ForMember(dst => dst.OrderDetails, src => src.MapFrom(o => o.OrderDetails))
            .ReverseMap();
            mc.CreateMap<CreateGuestShippingOrderRequestModel, CreateZaloPayOrderRequestModel>()
            .ForMember(dst => dst.Description, o => o.Ignore())
            .ForMember(dst => dst.RedirectUrl, o => o.Ignore())
            .ForMember(dst => dst.OrderDetails, src => src.MapFrom(o => o.OrderDetails))
            .ReverseMap();
            #endregion
        }
    }
}
