using Boek.Core.Constants;
using Boek.Core.Enums;
using Boek.Infrastructure.Requests.Orders;
using Boek.Infrastructure.Requests.Orders.Update;
using Boek.Infrastructure.ViewModels.Orders;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Boek.Api.Controllers.Issuer
{
    [Route("api/issuer/orders")]
    [ApiController]
    [Authorize(Roles = nameof(BoekRole.Issuer))]
    public class IssuerOrdersController : ControllerBase
    {
        #region Field(s) and constructor
        private readonly IOrderService _orderService;

        public IssuerOrdersController(IOrderService orderService)
        {
            this._orderService = orderService;
        }
        #endregion

        #region Gets
        /// <summary>
        /// Get all orders
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IQueryable<OrderViewModel>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_ORDER })]
        public async Task<IActionResult> GetOrders([FromQuery] OrderRequestModel filter, [FromQuery] PagingModel paging)
        {
            try
            {
                return StatusCode(200, await _orderService.GetOrdersByIssuer(filter, paging));
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
        /// Get order by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderViewModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_ORDER })]
        public ActionResult<OrderViewModel> GetOrder(Guid id)
        {
            return _orderService.GetOrderByIssuer(id);
        }

        /// <summary>
        /// Get campaign's addresses by order id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("campaigns/addresses/{id}")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_ORDER })]
        public ActionResult<List<string>> GetOrderCampaignAddresses(Guid id)
        {
            return _orderService.GetOrderCampaignAddresses(id);
        }
        #endregion

        #region Update

        /// <summary>
        /// Update the address of a pick-up available order
        /// </summary>
        /// <param name="updateOrder"></param>
        /// <returns></returns>
        [HttpPut("available/address")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_ORDER })]
        public ActionResult<OrderViewModel> UpdateAddressPickUpOrder([FromBody] UpdateAvailableOrderRequestModel updateOrder)
        {
            var result = _orderService.UpdateAddressPickUpOrder(updateOrder);
            _orderService.SendUpdateAddressPickUpOrderEmail(result, updateOrder.Note);
            return result;
        }
        /// <summary>
        /// Update a pick-up available order
        /// </summary>
        /// <param name="updateOrder"></param>
        /// <returns></returns>
        [HttpPut("available")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_ORDER })]
        public ActionResult<OrderViewModel> PutPickUpAvailableStatusOrder([FromBody] UpdateAvailableOrderRequestModel updateOrder)
        {
            var result = _orderService.UpdatePickUpAvailableStatusOrder(updateOrder);
            _orderService.SendAvailableOrderEmail(result, updateOrder.Note);
            return result;
        }

        /// <summary>
        /// Update a shipping order
        /// </summary>
        /// <param name="updateOrder"></param>
        /// <returns></returns>
        [HttpPut("shipping")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_ORDER })]
        public ActionResult<OrderViewModel> PutShippingStatusOrder([FromBody] OrderUpdateModel updateOrder)
        {
            var result = _orderService.UpdateShippingStatusOrder(updateOrder);
            _orderService.SendAvailableOrderEmail(result, updateOrder.Note);
            return result;
        }

        /// <summary>
        /// Update a shipped order
        /// </summary>
        /// <param name="updateOrder"></param>
        /// <returns></returns>
        [HttpPut("shipped")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_ORDER })]
        public ActionResult<OrderViewModel> PutShippedStatusOrder([FromBody] OrderUpdateModel updateOrder)
        {
            var result = _orderService.UpdateShippedStatusOrder(updateOrder);
            _orderService.SendShippedOrderEmail(result, updateOrder.Note);
            return result;
        }

        /// <summary>
        /// Update a cancel order by issuer
        /// </summary>
        /// <param name="updateOrder"></param>
        /// <returns></returns>
        [HttpPut("cancel")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_ORDER })]
        public ActionResult<OrderViewModel> PutCancelStatusOrderByIssuer([FromBody] OrderUpdateModel updateOrder)
        {
            var result = _orderService.UpdateCancelStatusOrderByIssuer(updateOrder);
            _orderService.SendCancelledOrderByIssuerNotification(result);
            _orderService.SendIssuerCancelledOrderEmail(result, updateOrder.Note);
            return result;
        }
        #endregion
    }
}