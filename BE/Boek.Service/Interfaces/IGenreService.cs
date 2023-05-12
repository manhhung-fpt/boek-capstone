using Boek.Infrastructure.Requests.Genres;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.Genres;
using Boek.Service.Commons;

namespace Boek.Service.Interfaces
{
    public interface IGenreService
    {
        /// <summary>
        /// Get all genres (<paramref name="filter"/>, <paramref name="paging"/>, <paramref name="WithBooks"/>)
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <param name="WithBooks"></param>
        /// <returns>
        /// If a request includes WithBooks, then it returns all genres with books respectively. Otherwise, it returns just genres only.
        /// </returns>
        BaseResponsePagingModel<GenreBooksViewModel> GetGenres(GenreRequestModel filter, PagingModel paging, bool WithBooks = false);

        /// <summary>
        /// Get a genre by id (<paramref name="id"/>, <paramref name="WithBooks"/>)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// If a request includes WithBooks, then it returns a matched genre with books. Otherwise, it returns just a matched genre only.
        /// </returns>
        /// <exception cref="ErrorResponse">
        /// Throw a ErrorResponse if there is no matched genre
        /// </exception>
        GenreBooksViewModel GetGenreById(int id, bool WithBooks = false);

        /// <summary>
        /// Get child genres
        /// </summary>
        /// <returns></returns>
        List<GenreViewModel> GetChildGenres();
        /// <summary>
        /// Create a genre (<paramref name="createGenre"/>)
        /// </summary>
        /// <param name="createGenre"></param>
        /// <returns>If a genre is valid, it returns a created genre's detail</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched genre
        /// 2. Throw a ErrorResponse if genre name is null or empty
        /// 3. Throw a ErrorResponse if there is no matched parent genre request
        /// 4. Throw a ErrorResponse if creating a genre is failed
        /// </exception>
        GenreViewModel CreateGenre(CreateGenreRequestModel createGenre);

        /// <summary>
        /// Update a genre (<paramref name="updateGenre"/>)
        /// </summary>
        /// <param name="updateGenre"></param>
        /// <returns>If a genre is valid, it returns an updated genre's detail</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched genre
        /// 2. Throw a ErrorResponse if genre name is null or empty
        /// 3. Throw a ErrorResponse if there is no matched parent genre request
        /// 4. Throw a ErrorResponse if genre's name is duplicated
        /// 5. Throw a ErrorResponse if updating a genre is failed because it links to other information
        /// 6. Throw a ErrorResponse if updating a genre is failed
        /// </exception>
        GenreViewModel UpdateGenre(UpdateGenreRequestModel updateGenre);

        /// <summary>
        /// Delete a genre (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>If a genre is valid, it returns a Deleted genre's detail</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched genre
        /// 2. Throw a ErrorResponse if a genre links to other information
        /// 3. Throw a ErrorResponse if deleting a genre is failed
        /// </exception>
        GenreViewModel DeleteGenre(int id);

        /// <summary>
        /// Get other genres for campaign commission (<paramref name="campaignId"/>)
        /// </summary>
        /// <param name="campaignId"></param>
        /// <returns></returns>
        /// <exception cref="ErrorResponse">
        /// Throw a ErrorResponse if there is no matched campaign
        /// </exception>
        List<GenreViewModel> GetOtherGenresForCampaignCommission(int campaignId);
    }
}
