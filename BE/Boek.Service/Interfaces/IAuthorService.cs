using Boek.Infrastructure.Requests.Authors;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.Authors;
using Boek.Service.Commons;

namespace Boek.Service.Interfaces
{
    public interface IAuthorService
    {
        /// <summary>
        /// Get authors
        /// (<paramref name="filter"/>,<paramref name="paging"/>,<paramref name="WithBooks"/>)
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <param name="WithBooks"></param>
        /// <returns>
        /// If a request includes WithBooks, then it returns all authors with books respectively. Otherwise, it returns just authors only.
        /// </returns>
        BaseResponsePagingModel<AuthorBooksViewModel> GetAuthors(AuthorViewModel filter, PagingModel paging, bool WithBooks = false);
        /// <summary>
        /// Get an author by id
        /// (<paramref name="authorId"/>,<paramref name="WithBooks"/>)
        /// </summary>
        /// <param name="authorId"></param>
        /// <param name="WithBooks"></param>
        /// /// <exception cref="ErrorResponse">
        /// Throw a ErrorResponse if there is no matched author.
        /// </exception>
        /// <returns>
        /// If a request includes WithBooks, then it returns a matched author with books. Otherwise, it returns just a matched author only.
        /// </returns>
        AuthorBooksViewModel GetAuthorById(int authorId, bool WithBooks = false);
        /// <summary>
        /// Update an author (<paramref name="updatedAuthor"/>)
        /// </summary>
        /// <param name="updatedAuthor"></param>
        /// <returns>If an author is valid, then it returns the result of updated author</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched author.
        /// 2. Throw a ErrorResponse if there is a duplicated author's name.
        /// 3. Throw a ErrorResponse if updating an author is failed.
        /// </exception>
        AuthorViewModel UpdateAuthor(UpdateAuthorRequestModel updatedAuthor);
        /// <summary>
        /// Create an author (<paramref name="createdAuthor"/>)
        /// </summary>
        /// <param name="createdAuthor"></param>
        /// <returns>If an author is valid, then it returns the result of created author</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is a duplicated author's name.
        /// 2. Throw a ErrorResponse if creating an author is failed.
        /// </exception>
        AuthorViewModel CreateAuthor(CreateAuthorRequestModel createdAuthor);
        /// <summary>
        /// Create an author (<paramref name="createdAuthor"/>)
        /// </summary>
        /// <param name="createdAuthor"></param>
        /// <returns>If an author is valid, then it returns the result of created author</returns>
        /// /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is a duplicated author's name.
        /// 2. Throw a ErrorResponse if creating an author is failed.
        /// </exception>
        AuthorViewModel CreateAuthorByIssuer(CreateAuthorByIssuerRequestModel createdAuthor);
        /// <summary>
        /// Delete an author (<paramref name="authorId"/>)
        /// </summary>
        /// <param name="authorId"></param>
        /// <returns>If an author is valid, then it returns the result of deleted author</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched author.
        /// 2. Throw a ErrorResponse if this author is linked to other information.
        /// 3. Throw a ErrorResponse if deleting an author is failed.
        /// </exception>
        AuthorViewModel DeleteAuthor(int? authorId);
    }
}
