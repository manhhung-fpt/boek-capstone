using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Boek.Core.Entities;
using Boek.Service.Interfaces;
using Boek.Core.Constants;
using Boek.Infrastructure.Requests.Orders;
using Boek.Service.Commons;
using Boek.Infrastructure.ViewModels.Orders;
using Boek.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Boek.Infrastructure.Responds.Orders;
using Boek.Infrastructure.Requests.Orders.Guest;
using Boek.Infrastructure.Requests.Orders.ZaloPay;
using Boek.Infrastructure.Requests.Orders.Calculation;
using Boek.Infrastructure.ViewModels.Orders.Calculation;

namespace Boek.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        #region Field(s) and constructor
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            this._orderService = orderService;
        }
        #endregion

        #region Gets
        /// <summary>
        /// Get orders by customer
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IQueryable<Order>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Authorize(Roles = nameof(BoekRole.Customer))]
        public async Task<IActionResult> GetOrdersByCustomer([FromQuery] OrderRequestModel filter, [FromQuery] PagingModel paging)
        {
            try
            {
                return StatusCode(200, await _orderService.GetOrdersByCustomer(filter, paging));
            }
            catch (ApplicationException)
            {
                return StatusCode(400, ErrorMessageConstants.NOT_FOUND);
            }
            catch (Exception)
            {
                return StatusCode(500, ErrorMessageConstants.INTERNAL_ERROR);
            }
        }

        /// <summary>
        /// Get order by customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderViewModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Authorize(Roles = nameof(BoekRole.Customer))]
        public ActionResult<OrderViewModel> GetOrderByCustomer(Guid id)
        {
            return _orderService.GetOrderByCustomer(id);
        }
        #endregion

        #region Calculation
        /// <summary>
        /// Calculate a shipping order
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("calculation/shipping")]
        public ActionResult<OrderCalculationViewModel> GetShippingOrderCalculation([FromBody] ShippingOrderCalculationRequestModel request)
        {
            return _orderService.GetShippingOrderCalculation(request);
        }
        /// <summary>
        /// Calculate a pick-up order
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("calculation/pick-up")]
        public ActionResult<OrderCalculationViewModel> GetPickUpOrderCalculation([FromBody] PickUpOrderCalculationRequestModel request)
        {
            return _orderService.GetPickUpOrderCalculation(request);
        }
        /// <summary>
        /// Calculate the total of cart
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("calculation/cart")]
        public ActionResult<OrderCalculationViewModel> GetCartCalculation([FromBody] PickUpOrderCalculationRequestModel request)
        {
            return _orderService.GetCartCalculation(request);
        }
        #endregion

        #region Adds
        /// <summary>
        /// Create multiple orders
        /// </summary>
        /// <param name="createOrder"></param>
        /// <returns></returns>
        [HttpPost("range")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Authorize(Roles = nameof(BoekRole.Customer))]
        public async Task<IActionResult> PostOrders(IEnumerable<OrderCreateModel> createOrder)
        {
            try
            {
                await _orderService.AddRangeAsyncCustom(createOrder);
                return StatusCode(201, ErrorMessageConstants.INSERT + " " + MessageConstants.MESSAGE_SUCCESS);
            }
            catch (ApplicationException)
            {
                return StatusCode(400, ErrorMessageConstants.INSERT + " " + MessageConstants.MESSAGE_FAILED);
            }
            catch (Exception)
            {
                return StatusCode(500, ErrorMessageConstants.INTERNAL_ERROR);
            }
        }

        #region Customer
        /// <summary>
        /// Create shipping order by customer 
        /// </summary>
        /// <param name="createShippingOrder"></param>
        /// <returns></returns>
        [HttpPost("customer/shipping")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Authorize(Roles = nameof(BoekRole.Customer))]
        public ActionResult<List<OrderViewModel>> CreateShippingOrderByCustomer(CreateShippingOrderRequestModel createShippingOrder)
        {
            var result = _orderService.CreateShippingOrderByCustomer(createShippingOrder);
            _orderService.SendNewShippingOrderNotification(result);
            var pairs = _orderService.ConvertOrdersWithNote(result);
            _orderService.SendNewShippingOrderEmail(pairs);
            return result;
        }

        /// <summary>
        /// Create pick up order by customer 
        /// </summary>
        /// <param name="createPickUpOrder"></param>
        /// <returns></returns>
        [HttpPost("customer/pick-up")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Authorize(Roles = nameof(BoekRole.Customer))]
        public ActionResult<List<OrderViewModel>> CreatePickUpOrderByCustomer(CreateCustomerPickUpOrderRequestModel createPickUpOrder)
        {
            var result = _orderService.CreatePickUpOrderByCustomer(createPickUpOrder);
            _orderService.SendNewPickUpOrderNotification(result);
            var pairs = _orderService.ConvertOrdersWithNote(result);
            _orderService.SendNewPickUpOrderEmail(pairs);
            return result;
        }
        #endregion

        #region Guest
        /// <summary>
        /// Create shipping order by guest 
        /// </summary>
        /// <param name="createShippingOrder"></param>
        /// <returns></returns>
        [HttpPost("guest/shipping")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Authorize(Roles = nameof(BoekRole.Guest))]
        public ActionResult<List<OrderViewModel>> CreateShippingOrderByGuest(CreateGuestShippingOrderRequestModel createShippingOrder)
        {
            var result = _orderService.CreateShippingOrderByGuest(createShippingOrder);
            _orderService.SendNewShippingOrderNotification(result);
            var pairs = _orderService.ConvertOrdersWithNote(result);
            _orderService.SendNewShippingOrderEmail(pairs);
            return result;
        }

        /// <summary>
        /// Create pick up order by guest 
        /// </summary>
        /// <param name="createPickUpOrder"></param>
        /// <returns></returns>
        [HttpPost("guest/pick-up")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Authorize(Roles = nameof(BoekRole.Guest))]
        public ActionResult<List<OrderViewModel>> CreatePickUpOrderByGuest(CreateGuestPickUpOrderRequestModel createPickUpOrder)
        {
            var result = _orderService.CreatePickUpOrderByGuest(createPickUpOrder);
            _orderService.SendNewPickUpOrderNotification(result);
            var pairs = _orderService.ConvertOrdersWithNote(result);
            _orderService.SendNewPickUpOrderEmail(pairs);
            return result;
        }
        #endregion

        #endregion

        #region Updates
        /// <summary>
        /// Update multiple orders
        /// </summary>
        /// <param name="createOrder"></param>
        /// <returns></returns>
        [HttpPut("range")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutOrders(IEnumerable<OrderCreateModel> createOrder)
        {
            try
            {
                await _orderService.UpdateRangeCustom(createOrder);
                return StatusCode(204, ErrorMessageConstants.UPDATE + " " + MessageConstants.MESSAGE_SUCCESS);
            }
            catch (ApplicationException)
            {
                return StatusCode(400, ErrorMessageConstants.UPDATE + " " + MessageConstants.MESSAGE_FAILED);
            }
            catch (Exception)
            {
                return StatusCode(500, ErrorMessageConstants.INTERNAL_ERROR);
            }
        }

        /// <summary>
        /// Update a cancel order by customer
        /// </summary>
        /// <param name="updateOrder"></param>
        /// <returns></returns>
        [HttpPut("cancel")]
        [Authorize(Roles = nameof(BoekRole.Customer))]
        public ActionResult<OrderViewModel> PutCancelStatusOrderByCustomer([FromBody] OrderUpdateModel updateOrder)
        {
            var result = _orderService.UpdateCancelStatusOrderByCustomer(updateOrder);
            _orderService.SendCancelledOrderByCustomerNotification(result);
            _orderService.SendCustomerCancelledOrderEmail(result, updateOrder.Note);
            return result;
        }
        #endregion

        #region ZaloPay
        /// <summary>
        /// Get ZaloPay Order
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        [HttpGet("zalopay")]
        [Authorize(Roles = nameof(BoekRole.Customer))]
        public ActionResult<ZaloPayQueryCreatedOrderResponseModel> QueryZaloPayOrder([FromQuery] ZaloPayOrderQueryModel queryModel)
        {
            return _orderService.QueryCreatedZaloPayOrder(queryModel);
        }

        /// <summary>
        /// Create ZaloPay Order
        /// </summary>
        /// <param name="createZaloPayOrder"></param>
        /// <returns></returns>
        [HttpPost("zalopay")]
        [Authorize]
        public ActionResult<ZaloPayOrderResponseModel> PostZaloPayOrder([FromBody] CreateZaloPayOrderRequestModel createZaloPayOrder)
        {
            var orders = _orderService.ValidateZaloPayOrder(createZaloPayOrder);
            var createModel = _orderService.CreatePendingOrders(orders, createZaloPayOrder);
            var response = _orderService.CreateZaloPayOrder(createModel);
            if (response.return_code != 1)
                return BadRequest(response);
            return response;
        }

        /// <summary>
        /// ZaloPay Callback
        /// </summary>
        /// <param name="cbdata"></param>
        /// <returns></returns>
        [EnableCors("AllOrigins")]
        [HttpPost("zalopay/call-back")]
        public ActionResult<ZaloPayCallBackResponseModel> PostZaloPayCallBack([FromBody] dynamic cbdata)
        {
            var result = (ZaloPayCallPayViewModel)_orderService.CallBackZaloPayOrder(cbdata);
            if (result != null)
            {
                if (result.IsValid())
                {
                    var orders = _orderService.UpdateProgressingStatusOrder(result.orderUpdateModels);
                    if (orders != null)
                    {
                        if (orders.Any())
                        {
                            _orderService.SendNewZaloPayOrderNotification(orders);
                            var pairs = _orderService.ConvertOrdersWithNote(orders);
                            _orderService.SendNewZaloPayOrderEmail(pairs);
                        }
                    }
                }
                return result.zaloPayCallBackResponseModel;
            }
            return null;
        }
        #endregion
    }
}
