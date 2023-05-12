using Boek.Infrastructure.Requests.BookProducts;
using Boek.Infrastructure.ViewModels.BookProducts;
using Boek.Service.Interfaces;
using Boek.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Boek.Core.Constants;

namespace Boek.Api.Controllers.Admin
{
    [Route("api/admin/book-products")]
    [ApiController]
    [Authorize(Roles = nameof(BoekRole.Admin))]
    public class AdminBookProductsController : ControllerBase
    {
        #region Field(s) and constructor
        private readonly IBookProductService _bookProductService;
        public AdminBookProductsController(IBookProductService bookProductService)
        {
            _bookProductService = bookProductService;
        }
        #endregion

        #region Checks
        /// <summary>
        /// Accept book product by admin
        /// </summary>
        /// <param name="checkBook"></param>
        /// <returns></returns>
        [HttpPut("acceptance")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_BOOK_PRODUCT })]
        public ActionResult<BookProductViewModel> AcceptBookProduct([FromBody] CheckBookProductRequestModel checkBook)
        {
            var result = _bookProductService.AcceptBookProduct(checkBook);
            _bookProductService.SendDoneCheckingNotification(result);
            return result;
        }

        /// <summary>
        /// Reject book product by admin
        /// </summary>
        /// <param name="checkBook"></param>
        /// <returns></returns>
        [HttpPut("rejection")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_BOOK_PRODUCT })]
        public ActionResult<BookProductViewModel> RejectBookProduct([FromBody] CheckBookProductRequestModel checkBook)
        {
            var result = _bookProductService.RejectBookProduct(checkBook);
            _bookProductService.SendDoneCheckingNotification(result);
            return result;
        }
        #endregion
    }
}
