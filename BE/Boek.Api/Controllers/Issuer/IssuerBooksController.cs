using Boek.Core.Constants;
using Boek.Core.Enums;
using Boek.Infrastructure.Requests.BookProducts;
using Boek.Infrastructure.Requests.BookProducts.BookComboProducts;
using Boek.Infrastructure.Requests.BookProducts.BookSeriesProducts;
using Boek.Infrastructure.Requests.Books;
using Boek.Infrastructure.Requests.Books.BookSeries;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.BookProducts;
using Boek.Infrastructure.ViewModels.Books;
using Boek.Infrastructure.ViewModels.Books.Issuers;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Boek.Api.Controllers.Issuer
{
    [Route("api/issuer/books")]
    [ApiController]
    [Authorize(Roles = nameof(BoekRole.Issuer))]
    public class IssuerBooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IBookProductService _bookProductService;
        public IssuerBooksController(IBookService bookService,
        IBookProductService bookProductService)
        {
            _bookService = bookService;
            _bookProductService = bookProductService;
        }

        #region Books
        /// <summary>
        /// Get all books by issuer
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_BOOK_PRODUCT })]
        public ActionResult<BaseResponsePagingModel<BookViewModel>> GetBooks([FromQuery] BookRequestModel filter,
            [FromQuery] PagingModel paging)
        {
            return _bookService.GetBooksByIssuer(filter, paging);
        }
        /// <summary>
        /// Get all books for odd or series book products by issuer
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet("products/odd-series")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_BOOK_PRODUCT })]
        public ActionResult<BaseResponsePagingModel<BookViewModel>> GetBooksForOddOrSeriesBookProduct([FromQuery] SearchBookRequestModel filter,
            [FromQuery] PagingModel paging)
        {
            return _bookService.GetBooksForOddOrSeriesBookProductByIssuer(filter, paging);
        }

        /// <summary>
        /// Get a book by id by issuer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="WithCampaigns"></param>
        /// <param name="WithAllowChangingGenre"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_BOOK_PRODUCT })]
        public ActionResult<IssuerBookViewModel> GetBookById(int id, bool WithCampaigns = false, bool WithAllowChangingGenre = false)
        {
            return _bookService.GetBookByIdByIssuer(id, WithCampaigns, WithAllowChangingGenre);
        }

        #region Creates
        /// <summary>
        /// Create a book by issuer
        /// </summary>
        /// <param name="createdBook"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_BOOK_PRODUCT })]
        public ActionResult<BookViewModel> CreateBook([FromBody] CreateBookRequestModel createdBook)
        {
            //var result = CheckIssuer(id, createdBook.IssuerId);
            return _bookService.CreateBook(createdBook);
        }
        /// <summary>
        /// Create a book series by issuer
        /// </summary>
        /// <param name="createBookSeries"></param>
        /// <returns></returns>
        [HttpPost("series-books")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_BOOK_PRODUCT })]
        public ActionResult<BookViewModel> CreateBookSeries([FromBody] CreateBookSeriesRequestModel createBookSeries)
        {
            return _bookService.CreateBookSeries(createBookSeries);
        }
        #endregion

        #region Update
        /// <summary>
        /// Update a book by issuer
        /// </summary>
        /// <param name="updatedBook"></param>
        /// <returns></returns>
        [HttpPut]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_BOOK_PRODUCT })]
        public ActionResult<BookViewModel> UpdateBook([FromBody] UpdateBookRequestModel updatedBook)
        {
            return _bookService.UpdateBook(updatedBook);
        }
        /// <summary>
        /// Update a book series by issuer
        /// </summary>
        /// <param name="updateBookSeries"></param>
        /// <returns></returns>
        [HttpPut("series-books")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_BOOK_PRODUCT })]
        public ActionResult<BookViewModel> UpdateBookSeries([FromBody] UpdateBookSeriesRequestModel updateBookSeries)
        {
            return _bookService.UpdateBookSeries(updateBookSeries);
        }
        #endregion

        /// <summary>
        /// Enable a book by issuer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("enabled-book/{id}")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_BOOK_PRODUCT })]
        public ActionResult<BookViewModel> EnableBook(int id)
        {
            return _bookService.EnableBook(id);
        }

        /// <summary>
        /// Disable a book by issuer
        /// </summary>
        /// <param name="id"></param>s
        /// <returns></returns>
        [HttpPatch("disabled-book/{id}")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_BOOK_PRODUCT })]
        public ActionResult<BookViewModel> DisableBook(int id)
        {
            return _bookService.DisableBook(id);
        }
        #endregion

        #region Book Products
        #region Gets
        /// <summary>
        /// Get all book products by issuer
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet("products")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_BOOK_PRODUCT })]
        public ActionResult<BaseResponsePagingModel<BookProductViewModel>> GetBookProductsByIssuer([FromQuery] BookProductRequestModel filter,
            [FromQuery] PagingModel paging)
        {
            return _bookProductService.GetBookProductsByIssuer(filter, paging);
        }

        /// <summary>
        /// Get an issuer's book product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("products/{id}")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_BOOK_PRODUCT })]
        public ActionResult<BookProductViewModel> GetBookProductByIdByIssuer(Guid id)
        {
            return _bookProductService.GetBookProductByIdByIssuer(id);
        }

        /// <summary>
        /// Get existed combo book products by issuer
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("products/existed-combos")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_BOOK_PRODUCT })]
        public ActionResult<List<BookProductViewModel>> GetExistedComboBookProductsByIssuer([FromQuery] IssuerComboBookProductRequestModel filter)
        {
            return _bookProductService.GetExistedComboBookProductsByIssuer(filter);
        }
        #endregion

        #region Odd Books
        /// <summary>
        /// Create a book product by issuer
        /// </summary>
        /// <param name="createdBookProduct"></param>
        /// <returns></returns>
        [HttpPost("products/odd-books")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_BOOK_PRODUCT })]
        public ActionResult<BookProductViewModel> CreateBookProduct([FromBody] CreateBookProductRequestModel createdBookProduct)
        {
            var result = _bookProductService.CreateBookProduct(createdBookProduct);
            _bookProductService.SendCheckingNotification(result);
            return result;
        }

        /// <summary>
        /// Update a book product by issuer
        /// </summary>
        /// <param name="updatedBookProduct"></param>
        /// <returns></returns>
        [HttpPut("products/odd-books")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_BOOK_PRODUCT })]
        public ActionResult<BookProductViewModel> UpdateBookProduct([FromBody] UpdateBookProductRequestModel updatedBookProduct)
        {
            var result = _bookProductService.UpdateBookProduct(updatedBookProduct);
            _bookProductService.SendCheckingNotification(result);
            return result;
        }
        #endregion

        #region Combo Books
        /// <summary>
        /// Create a combo book product by issuer
        /// </summary>
        /// <param name="createBookCombo"></param>
        /// <returns></returns>
        [HttpPost("products/combo-books")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_BOOK_PRODUCT })]
        public ActionResult<BookProductViewModel> CreateBookComboProduct([FromBody] CreateBookComboProductRequestModel createBookCombo)
        {
            var result = _bookProductService.CreateBookComboProduct(createBookCombo);
            _bookProductService.SendCheckingNotification(result);
            return result;
        }

        /// <summary>
        /// Update a combo book product by issuer
        /// </summary>
        /// <param name="updateBookCombo"></param>
        /// <returns></returns>
        [HttpPut("products/combo-books")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_BOOK_PRODUCT })]
        public ActionResult<BookProductViewModel> UpdateBookComboProduct([FromBody] UpdateBookComboProductRequestModel updateBookCombo)
        {
            var result = _bookProductService.UpdateBookComboProduct(updateBookCombo);
            _bookProductService.SendCheckingNotification(result);
            return result;
        }
        #endregion

        #region Series Books
        /// <summary>
        /// Create a series book product by issuer
        /// </summary>
        /// <param name="createBookSeries"></param>
        /// <returns></returns>
        [HttpPost("products/series-books")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_BOOK_PRODUCT })]
        public ActionResult<BookProductViewModel> CreateBookSeriesProduct([FromBody] CreateBookSeriesProductRequestModel createBookSeries)
        {
            var result = _bookProductService.CreateBookSeriesProduct(createBookSeries);
            _bookProductService.SendCheckingNotification(result);
            return result;
        }

        /// <summary>
        /// Update a series book product by issuer
        /// </summary>
        /// <param name="updateBookSeries"></param>
        /// <returns></returns>
        [HttpPut("products/series-books")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_BOOK_PRODUCT })]
        public ActionResult<BookProductViewModel> UpdateBookSeriesProduct([FromBody] UpdateBookSeriesProductRequestModel updateBookSeries)
        {
            var result = _bookProductService.UpdateBookSeriesProduct(updateBookSeries);
            _bookProductService.SendCheckingNotification(result);
            return result;
        }
        #endregion

        #region Update quantity
        /// <summary>
        /// Update book product when campaign starts
        /// </summary>
        /// <param name="updateBookProduct"></param>
        /// <returns></returns>
        [HttpPut("products/started-campaign")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_BOOK_PRODUCT })]
        public ActionResult<BookProductViewModel> UpdateBookProductStartedCampaign([FromBody] UpdateBookProductStartedCampaignRequestModel updateBookProduct)
        {
            return _bookProductService.UpdateBookProductStartedCampaign(updateBookProduct);
        }
        #endregion
        #endregion
    }
}
