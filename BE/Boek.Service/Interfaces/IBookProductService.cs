using Boek.Infrastructure.Requests.BookProducts;
using Boek.Infrastructure.Requests.BookProducts.BookComboProducts;
using Boek.Infrastructure.Requests.BookProducts.BookSeriesProducts;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.BookProducts;
using Boek.Service.Commons;

namespace Boek.Service.Interfaces
{
    public interface IBookProductService
    {
        #region Gets
        /// <summary>
        /// Get book products
        /// (<paramref name="filter"/>,<paramref name="paging"/>
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns>
        /// </returns>
        BaseResponsePagingModel<BookProductViewModel> GetBookProducts(BookProductRequestModel filter, PagingModel paging);
        /// <summary>
        /// Get a book product by id
        /// (<paramref name="bookProductId"/>)
        /// </summary>
        /// <param name="bookProductId"></param>
        /// /// <exception cref="ErrorResponse">
        /// Throw a ErrorResponse if there is no matched book product.
        /// </exception>
        /// <returns></returns>
        BookProductViewModel GetBookProductById(Guid id);
        /// <summary>
        /// Get an issuer's book products
        /// (<paramref name="filter"/>,<paramref name="paging"/>
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns>
        /// </returns>
        BaseResponsePagingModel<BookProductViewModel> GetBookProductsByIssuer(BookProductRequestModel filter, PagingModel paging);
        /// <summary>
        /// Get an issuer's book product by id
        /// (<paramref name="bookProductId"/>)
        /// </summary>
        /// <param name="bookProductId"></param>
        /// /// <exception cref="ErrorResponse">
        /// Throw a ErrorResponse if there is no matched book product.
        /// </exception>
        /// <returns></returns>
        BookProductViewModel GetBookProductByIdByIssuer(Guid id);
        /// <summary>
        /// Get an issuer's existed combo book products
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        List<BookProductViewModel> GetExistedComboBookProductsByIssuer(IssuerComboBookProductRequestModel filter);
        #endregion

        #region Creates
        /// <summary>
        /// Create a book product (<paramref name="createdBookProduct"/>)
        /// </summary>
        /// <param name="createdBookProduct"></param>
        /// <returns>If an book product is valid, then it returns the result of created book product</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is a duplicated book product's name.
        /// 2. Throw a ErrorResponse if creating an book product is failed.
        /// </exception>
        BookProductViewModel CreateBookProduct(CreateBookProductRequestModel createdBookProduct);
        /// <summary>
        /// Create a book combo product (<paramref name="createBookCombo"/>)
        /// </summary>
        /// <param name="createBookCombo"></param>
        /// <returns>If an book product is valid, then it returns the result of created book product</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is a duplicated book product's name.
        /// 2. Throw a ErrorResponse if creating an book product is failed.
        /// </exception>
        BookProductViewModel CreateBookComboProduct(CreateBookComboProductRequestModel createBookCombo);
        /// <summary>
        /// Create a book series product (<paramref name="createBookSeries"/>)
        /// </summary>
        /// <param name="createBookSeries"></param>
        /// <returns>If an book product is valid, then it returns the result of created book product</returns>
        /// /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is a duplicated book product's name.
        /// 2. Throw a ErrorResponse if creating an book product is failed.
        /// </exception>
        BookProductViewModel CreateBookSeriesProduct(CreateBookSeriesProductRequestModel createBookSeries);
        #endregion

        #region Updates
        /// <summary>
        /// Update a book product (<paramref name="updatedBookProduct"/>)
        /// </summary>
        /// <param name="updatedBookProduct"></param>
        /// <returns>If an book product is valid, then it returns the result of updated book product</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched book product.
        /// 2. Throw a ErrorResponse if there is a duplicated book product's name.
        /// 3. Throw a ErrorResponse if updating an book product is failed.
        /// </exception>
        BookProductViewModel UpdateBookProduct(UpdateBookProductRequestModel updatedBookProduct);
        /// <summary>
        /// Update a book combo product (<paramref name="updateBookCombo"/>)
        /// </summary>
        /// <param name="updateBookCombo"></param>
        /// <returns>If an book product is valid, then it returns the result of updated book product</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched book product.
        /// 2. Throw a ErrorResponse if there is a duplicated book product's name.
        /// 3. Throw a ErrorResponse if updating an book product is failed.
        /// </exception>
        BookProductViewModel UpdateBookComboProduct(UpdateBookComboProductRequestModel updateBookCombo);
        /// <summary>
        /// Update a book series product (<paramref name="updateBookSeries"/>)
        /// </summary>
        /// <param name="updateBookSeries"></param>
        /// <returns>If an book product is valid, then it returns the result of updated book product</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched book product.
        /// 2. Throw a ErrorResponse if there is a duplicated book product's name.
        /// 3. Throw a ErrorResponse if updating an book product is failed.
        /// </exception>
        BookProductViewModel UpdateBookSeriesProduct(UpdateBookSeriesProductRequestModel updateBookSeries);

        BookProductViewModel UpdateBookProductStartedCampaign(UpdateBookProductStartedCampaignRequestModel updateBookProduct);
        #endregion

        #region Check
        /// <summary>
        /// Accept book product by admin (<paramref name="checkBook"/>)
        /// </summary>
        /// <param name="checkBook"></param>
        /// <returns></returns>
        BookProductViewModel AcceptBookProduct(CheckBookProductRequestModel checkBook);
        /// <summary>
        /// Reject book product by admin (<paramref name="checkBook"/>)
        /// </summary>
        /// <param name="checkBook"></param>
        /// <returns></returns>
        BookProductViewModel RejectBookProduct(CheckBookProductRequestModel checkBook);
        #endregion

        #region Notification
        /// <summary>
        /// Send checking book product notification
        /// </summary>
        /// <param name="bookProduct"></param>
        void SendCheckingNotification(BookProductViewModel bookProduct);
        /// <summary>
        /// Send done checking book product notification
        /// </summary>
        /// <param name="bookProduct"></param>
        void SendDoneCheckingNotification(BookProductViewModel bookProduct);
        #endregion

    }
}