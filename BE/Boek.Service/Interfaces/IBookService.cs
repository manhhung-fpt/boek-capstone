using Boek.Infrastructure.Requests.Books;
using Boek.Infrastructure.Requests.Books.BookSeries;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.Books;
using Boek.Infrastructure.ViewModels.Books.Issuers;
using Boek.Service.Commons;

namespace Boek.Service.Interfaces
{
    public interface IBookService
    {
        #region Gets
        /// <summary>
        /// Get all books (<paramref name="filter"/>, <paramref name="paging"/>)
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        BaseResponsePagingModel<BookViewModel> GetBooks(BookRequestModel filter, PagingModel paging);
        /// <summary>
        /// Get a book by id (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return a book's detail if found a book. Otherwise, it returns a not found message</returns>
        /// <exception cref="ErrorResponse">
        /// Throw a ErrorResponse if there is no matched book
        /// </exception>
        BookViewModel GetBookById(int id);
        /// <summary>
        /// Get an issuer's books (<paramref name="filter"/>, <paramref name="paging"/>)
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns>Return all books belonging to an issuer</returns>
        BaseResponsePagingModel<BookViewModel> GetBooksByIssuer(BookRequestModel filter, PagingModel paging);
        /// <summary>
        /// Get an issuer's books for book product (<paramref name="filter"/>, <paramref name="paging"/>)
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns>Return all books belonging to an issuer</returns>
        BaseResponsePagingModel<BookViewModel> GetBooksForOddOrSeriesBookProductByIssuer(SearchBookRequestModel filter, PagingModel paging);
        /// <summary>
        /// Get an issuer's book detail by id (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return a book's detail belonging to issuer. Otherwise, it returns a not found message</returns>
        /// <exception cref="ErrorResponse">
        /// Throw a ErrorResponse if there is no matched book
        /// </exception>
        IssuerBookViewModel GetBookByIdByIssuer(int id, bool WithCampaigns = false, bool WithAllowChangingGenre = false);
        /// <summary>
        /// Get books by genre (<paramref name="ParentGenreId"/>, <paramref name="GenreId"/>, <paramref name="paging"/>)
        /// </summary>
        /// <param name="ParentGenreId"></param>
        /// <param name="GenreId"></param>
        /// <param name="paging"></param>
        /// <returns>Return books based on parent genre or book's specific genre. Otherwise, it returns all books</returns>
        BaseResponsePagingModel<BookViewModel> GetBooksByParentGenre(int? ParentGenreId, int? GenreId, PagingModel paging);
        #endregion

        #region Creates
        /// <summary>
        /// Create an issuer's book (<paramref name="createdBook"/>)
        /// </summary>
        /// <param name="createdBook"></param>
        /// <returns>If an issuer's book is valid, it returns a created book's detail</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched publisher
        /// 2. Throw a ErrorResponse if there is a duplicated name of book
        /// 3. Throw a ErrorResponse if there is no format(s) of a book
        /// 4. Throw a ErrorResponse if there is no matched author
        /// 5. Throw a ErrorResponse if there is no matched genre
        /// 6. Throw a ErrorResponse if creating a book is failed
        /// </exception>
        BookViewModel CreateBook(CreateBookRequestModel createdBook);
        /// <summary>
        /// Create an issuer's book series (<paramref name="createBookSeries"/>)
        /// </summary>
        /// <param name="createdBook"></param>
        /// <returns>If an issuer's book series is valid, it returns a created book series's detail</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is a duplicated name of book series
        /// 2. Throw a ErrorResponse if there is no the matched format of a book series
        /// 3. Throw a ErrorResponse if there is no the matched book
        /// 4. Throw a ErrorResponse if there is no matched genre
        /// 5. Throw a ErrorResponse if the book's genre is unmatched with book series's genre
        /// 6. Throw a ErrorResponse if creating a book series is failed
        /// 7. Throw a ErrorResponse if creating a book series's items are failed 
        /// </exception>
        BookViewModel CreateBookSeries(CreateBookSeriesRequestModel createBookSeries);
        #endregion

        #region Updates
        /// <summary>
        /// Update an issuer's book (<paramref name="updatedBook"/>)
        /// </summary>
        /// <param name="updatedBook"></param>
        /// <returns>If an issuer's book is valid, it returns an updated book's detail</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched book
        /// 2. Throw a ErrorResponse if there is no matched publisher
        /// 3. Throw a ErrorResponse if there is a duplicated name of book
        /// 4. Throw a ErrorResponse if there is no format(s) of a book
        /// 6. Throw a ErrorResponse if there is no matched genre
        /// 7. Throw a ErrorResponse if a request issuer is not matched with a book's issuer
        /// 8. Throw a ErrorResponse if updating a book is failed
        /// 9. Throw a ErrorResponse if inserting the new author of a book is failed
        /// 10. Throw a ErrorResponse if deleting the unnecessary author of a book is failed
        /// 11. Throw a ErrorResponse if inserting the new format of a book is failed
        /// 12. Throw a ErrorResponse if inserting the unnecessary format of a book is failed
        /// </exception>
        BookViewModel UpdateBook(UpdateBookRequestModel updatedBook);
        /// <summary>
        /// Update an issuer's book series' (<paramref name="updateBookSeries"/>).
        /// </summary>
        /// <param name="updatedBook"></param>
        /// <returns>If an issuer's book is valid, it returns a created book series's detail</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched book
        /// 2. Throw a ErrorResponse if there is invalid format of book series
        /// 3. Throw a ErrorResponse if there is a duplicated name of book
        /// 4. Throw a ErrorResponse if there is no format(s) of a book
        /// 5. Throw a ErrorResponse if there is no matched author
        /// 6. Throw a ErrorResponse if there is no matched genre
        /// 7. Throw a ErrorResponse if the book's genre is unmatched with book series's genre
        /// 7. Throw a ErrorResponse if a request issuer is not matched with a book series's issuer
        /// 8. Throw a ErrorResponse if updating a book series is failed
        /// 9. Throw a ErrorResponse if inserting the new format of a book series is failed
        /// 10. Throw a ErrorResponse if inserting the unnecessary format of a book series is failed
        /// </exception>
        BookViewModel UpdateBookSeries(UpdateBookSeriesRequestModel updateBookSeries);
        #endregion

        #region Enable & Disable
        /// <summary>
        /// Enable an issuer book's status (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>If an issuer's book is valid, it returns an updated book's detail</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched book
        /// 2. Throw a ErrorResponse if a request issuer is not matched with a book's issuer
        /// 3. Throw a ErrorResponse if enabling a book is failed
        /// </exception>
        BookViewModel EnableBook(int id);
        /// <summary>
        /// Disable an issuer book's status (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>If an issuer's book is valid, it returns an updated book's detail</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched book
        /// 2. Throw a ErrorResponse if a request issuer is not matched with a book's issuer
        /// 3. Throw a ErrorResponse if disabling a book is failed
        /// </exception>
        BookViewModel DisableBook(int id);
        #endregion
    }
}
