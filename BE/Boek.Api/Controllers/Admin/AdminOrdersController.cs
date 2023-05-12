using Boek.Core.Constants;
using Boek.Core.Enums;
using Boek.Infrastructure.Requests.Orders;
using Boek.Infrastructure.ViewModels.Orders;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Boek.Api.Controllers.Admin
{
    [Route("api/admin/orders")]
    [ApiController]
    [Authorize(Roles = nameof(BoekRole.Admin))]
    public class AdminOrdersController : ControllerBase
    {
        #region Field(s) and constructor
        private readonly IOrderService _orderService;

        public AdminOrdersController(IOrderService orderService)
        {
            this._orderService = orderService;
        }
        #endregion

        #region Gets
        /// <summary>
        /// Get all orders by admin
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IQueryable<OrderViewModel>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_ORDER })]
        public async Task<IActionResult> GetOrders([FromQuery] OrderRequestModel filter, [FromQuery] PagingModel paging)
        {
            try
            {
                return StatusCode(200, await _orderService.GetOrders(filter, paging));
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
        /// Get order by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderViewModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_ORDER })]
        public ActionResult<OrderViewModel> GetOrder(Guid id)
        {
            return _orderService.GetOrder(id);
        }
        #endregion
    }
}