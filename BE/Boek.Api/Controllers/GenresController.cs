using Boek.Infrastructure.Requests.Genres;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.Genres;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Boek.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        #region Field(s) and constructor
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        #endregion

        #region Gets
        /// <summary>
        /// Get all genres
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <param name="WithBooks"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<BaseResponsePagingModel<GenreBooksViewModel>> GetGenres([FromQuery] GenreRequestModel filter, [FromQuery] PagingModel paging, [FromQuery] bool WithBooks = false)
        {
            return _genreService.GetGenres(filter, paging, WithBooks);
        }

        /// <summary>
        /// Get a genre by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="WithBooks"></param>
        [HttpGet("{id}")]
        public ActionResult<GenreBooksViewModel> GetGenreById(int id, [FromQuery] bool WithBooks = false)
        {
            return _genreService.GetGenreById(id, WithBooks);
        }

        /// <summary>
        /// Get child genres
        /// </summary>
        [HttpGet("child-genres")]
        public ActionResult<List<GenreViewModel>> GetChildGenres()
        {
            return _genreService.GetChildGenres();
        }
        #endregion
    }
}
