using Boek.Core.Constants;
using Boek.Infrastructure.Requests.BookProducts;
using Boek.Infrastructure.Requests.BookProducts.Mobile;
using Boek.Infrastructure.Requests.Books;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.BookProducts;
using Boek.Infrastructure.ViewModels.BookProducts.Mobile;
using Boek.Infrastructure.ViewModels.Books;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Boek.Service.Interfaces.Mobile;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Boek.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        #region Field(s) and constructor
        private readonly IBookService _bookService;
        private readonly IBookProductService _bookProductService;
        private readonly IBookProductMobileService _bookProductMobileService;
        public BooksController(IBookService bookService,
        IBookProductService bookProductService,
        IBookProductMobileService bookProductMobileService)
        {
            _bookService = bookService;
            _bookProductService = bookProductService;
            _bookProductMobileService = bookProductMobileService;
        }
        #endregion

        #region Books
        /// <summary>
        /// Get all books
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<BaseResponsePagingModel<BookViewModel>> GetBooks([FromQuery] BookRequestModel filter, [FromQuery] PagingModel paging)
        {
            return _bookService.GetBooks(filter, paging);
        }

        /// <summary>
        /// Get a book by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<BookViewModel> GetBookById(int id)
        {
            return _bookService.GetBookById(id);
        }

        /// <summary>
        /// Get books by genre
        /// </summary>
        /// <param name="ParentGenreId"></param>
        /// <param name="GenreId"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet("genre")]
        public ActionResult<BaseResponsePagingModel<BookViewModel>> GetBookByGenre([FromQuery] int? ParentGenreId, [FromQuery] int? GenreId, [FromQuery] PagingModel paging)
        {
            return _bookService.GetBooksByParentGenre(ParentGenreId, GenreId, paging);
        }
        #endregion

        #region Book Products
        /// <summary>
        /// Get all book products
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet("products")]
        public ActionResult<BaseResponsePagingModel<BookProductViewModel>> GetBookProducts([FromQuery] BookProductRequestModel filter, [FromQuery] PagingModel paging)
        {
            return _bookProductService.GetBookProducts(filter, paging);
        }

        /// <summary>
        /// Get a book product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("products/{id}")]
        public ActionResult<BookProductViewModel> GetBookProductById(Guid id)
        {
            return _bookProductService.GetBookProductById(id);
        }
        #endregion

        #region Customer Book Products
        /// <summary>
        /// Get a hierarchical book product
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("customer/products/hierarchical-book-products")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_CUSTOMER_BOOK_PRODUCT })]
        public ActionResult<HierarchicalBookProductsViewModel> GetHierarchicalBookProducts([FromQuery] HierarchicalBookProductsRequestModel filter)
        {
            return _bookProductMobileService.GetHierarchicalBookProducts(filter);
        }
        /// <summary>
        /// Get an unhierarchical book product
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("customer/products/unhierarchical-book-products")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_CUSTOMER_BOOK_PRODUCT })]
        public ActionResult<UnhierarchicalBookProductsViewModel> GetUnhierarchicalBookProducts([FromQuery] UnhierarchicalBookProductsRequestModel filter)
        {
            return _bookProductMobileService.GetUnhierarchicalBookProducts(filter);
        }
        /// <summary>
        /// Search customer book products
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet("customer/products")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_CUSTOMER_BOOK_PRODUCT })]
        public ActionResult<BaseResponsePagingModel<MobileBookProductViewModel>> GetMobileBookProducts([FromQuery] BookProductMobileRequestModel filter, [FromQuery] PagingModel paging)
        {
            return _bookProductMobileService.GetMobileBookProducts(filter, paging);
        }
        /// <summary>
        /// Get customer book product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("customer/products/{id}")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_CUSTOMER_BOOK_PRODUCT })]
        public ActionResult<MobileBookProductViewModel> GetMobileBookProductById(Guid? id)
        {
            return _bookProductMobileService.GetMobileBookProductById(id);
        }
        #endregion
    }
}
