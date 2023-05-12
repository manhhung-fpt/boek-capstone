using Boek.Core.Entities;
using Boek.Infrastructure.Requests.Orders;
using Boek.Infrastructure.Requests.Orders.Calculation;
using Boek.Infrastructure.Requests.Orders.Guest;
using Boek.Infrastructure.Requests.Orders.Update;
using Boek.Infrastructure.Requests.Orders.ZaloPay;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.Responds.Orders;
using Boek.Infrastructure.ViewModels.Orders;
using Boek.Infrastructure.ViewModels.Orders.Calculation;
using Boek.Service.Commons;

namespace Boek.Service.Interfaces
{
    public interface IOrderService
    {
        #region Gets
        OrderViewModel GetOrder(Guid id);
        OrderViewModel GetOrderByIssuer(Guid id);
        Task<BaseResponsePagingModel<OrderViewModel>> GetOrders(OrderRequestModel filter, PagingModel paging);
        Task<BaseResponsePagingModel<OrderViewModel>> GetOrdersByIssuer(OrderRequestModel filter, PagingModel paging);
        Task<BaseResponsePagingModel<OrderViewModel>> GetOrdersByStaff(OrderRequestModel filter, PagingModel paging, bool? byStaff = false);
        Task<List<OrderViewModel>> GetOrderFromQRCode(QROrdersRequestModel qROrders);
        Task<BaseResponsePagingModel<OrderViewModel>> GetOrdersByCustomer(OrderRequestModel filter, PagingModel paging);
        OrderViewModel GetOrderByStaff(Guid id);
        OrderViewModel GetOrderByCustomer(Guid id);
        List<string> GetOrderCampaignAddresses(Guid id);
        #endregion

        #region Calculation
        OrderCalculationViewModel GetShippingOrderCalculation(ShippingOrderCalculationRequestModel request);
        OrderCalculationViewModel GetPickUpOrderCalculation(PickUpOrderCalculationRequestModel request);
        OrderCalculationViewModel GetCartCalculation(PickUpOrderCalculationRequestModel request);
        #endregion

        #region Create
        Task<Order> AddOrderAsyncCustom(OrderCreateModel createOrder);
        Task AddRangeAsyncCustom(IEnumerable<OrderCreateModel> entity);

        #region Customer
        List<OrderViewModel> CreateShippingOrderByCustomer(CreateShippingOrderRequestModel createShippingOrder);
        List<OrderViewModel> CreatePickUpOrderByCustomer(CreateCustomerPickUpOrderRequestModel createPickUpOrder);
        #endregion

        #region Staff
        List<OrderViewModel> CreatePickUpOrderByStaff(CreateStaffPickUpOrderRequestModel createPickUpOrder);
        #endregion

        #region Guest
        List<OrderViewModel> CreateShippingOrderByGuest(CreateGuestShippingOrderRequestModel createShippingOrder);
        List<OrderViewModel> CreatePickUpOrderByGuest(CreateGuestPickUpOrderRequestModel createPickUpOrder);
        #endregion

        #endregion

        #region Update
        Task UpdateRangeCustom(IEnumerable<OrderCreateModel> entity);
        List<OrderViewModel> UpdateProgressingStatusOrder(List<OrderUpdateModel> orderUpdateModels);
        OrderViewModel UpdatePickUpAvailableStatusOrder(UpdateAvailableOrderRequestModel updateAvailableOrder);
        OrderViewModel UpdateShippingStatusOrder(OrderUpdateModel updateOrder);
        OrderViewModel UpdateShippedStatusOrder(OrderUpdateModel updateOrder);
        OrderViewModel UpdateReceivedStatusOrder(OrderUpdateModel updateOrder);
        OrderViewModel UpdateCancelStatusOrderByIssuer(OrderUpdateModel updateOrder);
        OrderViewModel UpdateCancelStatusOrderByCustomer(OrderUpdateModel updateOrder);
        OrderViewModel UpdateAddressPickUpOrder(UpdateAvailableOrderRequestModel updateAvailableOrder);
        #endregion

        #region ZaloPay
        List<Order> ValidateZaloPayOrder(CreateZaloPayOrderRequestModel createZaloPayOrder);
        ZaloPayCreateOrder CreatePendingOrders(List<Order> orders, CreateZaloPayOrderRequestModel createZaloPayOrder);
        ZaloPayOrderResponseModel CreateZaloPayOrder(ZaloPayCreateOrder createModel);
        ZaloPayQueryCreatedOrderResponseModel QueryCreatedZaloPayOrder(ZaloPayOrderQueryModel createModel);
        ZaloPayCallPayViewModel CallBackZaloPayOrder(dynamic cbdata);
        #endregion

        #region Notification
        /// <summary>
        /// Send new zalopay order notification
        /// </summary>
        /// <param name="orders"></param>
        void SendNewZaloPayOrderNotification(List<OrderViewModel> orders);
        /// <summary>
        /// Send new shipping order notification
        /// </summary>
        /// <param name="orders"></param>
        void SendNewShippingOrderNotification(List<OrderViewModel> orders);
        /// <summary>
        /// Send new pick-up order notification
        /// </summary>
        /// <param name="orders"></param>
        void SendNewPickUpOrderNotification(List<OrderViewModel> orders);
        /// <summary>
        /// Send cancelled order by issuer notification
        /// </summary>
        /// <param name="order"></param>
        void SendCancelledOrderByIssuerNotification(OrderViewModel order);
        /// <summary>
        /// Send cancelled order by customer notification
        /// </summary>
        /// <param name="order"></param>
        void SendCancelledOrderByCustomerNotification(OrderViewModel order);
        #endregion

        #region Email
        void SendNewZaloPayOrderEmail(Dictionary<OrderViewModel, string> orders);
        void SendNewShippingOrderEmail(Dictionary<OrderViewModel, string> orders);
        void SendNewPickUpOrderEmail(Dictionary<OrderViewModel, string> orders);
        void SendAvailableOrderEmail(OrderViewModel order, string Note);
        void SendUpdateAddressPickUpOrderEmail(OrderViewModel order, string Note);
        void SendShippingOrderEmail(OrderViewModel order, string Note);
        void SendReceivedOrderEmail(OrderViewModel order, string Note);
        void SendShippedOrderEmail(OrderViewModel order, string Note);
        void SendCustomerCancelledOrderEmail(OrderViewModel order, string Note);
        void SendIssuerCancelledOrderEmail(OrderViewModel order, string Note);
        void SendBoekCancelledOrderEmail(List<Guid> orderIds);
        #endregion

        #region Models
        Dictionary<OrderViewModel, string> ConvertOrdersWithNote(List<OrderViewModel> orders);
        #endregion
    }
}
