using Boek.Core.Constants;
using Boek.Core.Entities;
using Boek.Core.Enums;
using Boek.Infrastructure.Requests.Orders;
using Boek.Infrastructure.ViewModels.Orders;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Boek.Api.Controllers.Staff
{
    [Route("api/staff/orders")]
    [ApiController]
    [Authorize(Roles = nameof(BoekRole.Staff))]
    public class StaffOrdersController : ControllerBase
    {
        #region Field(s) and constructor
        private readonly IOrderService _orderService;

        public StaffOrdersController(IOrderService orderService)
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
        /// <param name="byStaff"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IQueryable<Order>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_STAFF_ORDER })]
        public async Task<IActionResult> GetOrders([FromQuery] OrderRequestModel filter, [FromQuery] PagingModel paging, [FromQuery] bool? byStaff = false)
        {
            try
            {
                return StatusCode(200, await _orderService.GetOrdersByStaff(filter, paging, byStaff));
            }
            catch (ApplicationException)
            {
                return StatusCode(400, ErrorMessageConstants.NOT_FOUND);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ErrorMessageConstants.INTERNAL_ERROR);
            }
        }
        /// <summary>
        /// Get all orders from QR
        /// </summary>
        /// <param name="qROrders"></param>
        /// <returns></returns>
        [HttpGet("qr")]
        [ProducesResponseType(typeof(IQueryable<Order>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_STAFF_ORDER })]
        public async Task<IActionResult> GetOrderFromQRCode([FromBody] QROrdersRequestModel qROrders)
        {
            try
            {
                return StatusCode(200, await _orderService.GetOrderFromQRCode(qROrders));
            }
            catch (ApplicationException)
            {
                return StatusCode(400, ErrorMessageConstants.NOT_FOUND);
            }
            catch (Exception)
            {
                return StatusCode(500, "Lỗi! Vui lòng liên hệ quản trị viên.");
            }
        }

        /// <summary>
        /// Get order by staff
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_STAFF_ORDER })]
        [ProducesResponseType(typeof(OrderViewModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult<OrderViewModel> GetOrderByStaff(Guid id)
        {
            return _orderService.GetOrderByStaff(id);
        }
        #endregion

        #region Add
        /// <summary>
        /// Create order by staff 
        /// </summary>
        /// <param name="createPickUpOrder"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_STAFF_ORDER })]
        public ActionResult<List<OrderViewModel>> CreatePickUpOrderByStaff(CreateStaffPickUpOrderRequestModel createPickUpOrder)
        {
            var result = _orderService.CreatePickUpOrderByStaff(createPickUpOrder);
            var pairs = _orderService.ConvertOrdersWithNote(result);
            _orderService.SendNewPickUpOrderEmail(pairs);
            return result;
        }
        #endregion

        #region Update
        /// <summary>
        /// Update a received order
        /// </summary>
        /// <param name="updateOrder"></param>
        /// <returns></returns>
        [HttpPut("received")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_STAFF_ORDER })]
        public ActionResult<OrderViewModel> PutReceivedStatusOrder([FromBody] OrderUpdateModel updateOrder)
        {
            var result = _orderService.UpdateReceivedStatusOrder(updateOrder);
            _orderService.SendReceivedOrderEmail(result, updateOrder.Note);
            return result;
        }
        #endregion
    }
}