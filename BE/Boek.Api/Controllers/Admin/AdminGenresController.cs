using Boek.Infrastructure.Requests.Genres;
using Boek.Infrastructure.ViewModels.Genres;
using Boek.Service.Interfaces;
using Boek.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Boek.Core.Constants;

namespace Boek.Api.Controllers.Admin
{
    [Route("api/admin/genres")]
    [ApiController]
    [Authorize(Roles = nameof(BoekRole.Admin))]
    public class AdminGenresController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public AdminGenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        #region Genres
        /// <summary>
        /// Create a genre by admin
        /// </summary>
        /// <param name="createGenre"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_GENRE })]
        public ActionResult<GenreViewModel> CreateGenre([FromBody] CreateGenreRequestModel createGenre)
        {
            return _genreService.CreateGenre(createGenre);
        }

        /// <summary>
        /// Update a genre by admin
        /// </summary>
        /// <param name="updateGenre"></param>
        /// <returns></returns>
        [HttpPut]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_GENRE })]
        public ActionResult<GenreViewModel> UpdateGenre([FromBody] UpdateGenreRequestModel updateGenre)
        {
            return _genreService.UpdateGenre(updateGenre);
        }
        #endregion

        #region Commission Genres
        #region Gets
        /// <summary>
        /// Get other commission genres
        /// </summary>
        /// <param name="campaignid"></param>
        /// <returns></returns>
        [HttpGet("other-commission-genres/{campaignid}")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_GENRE })]
        public ActionResult<List<GenreViewModel>> GetOtherGenresForCampaignCommission(int campaignid)
        {
            return _genreService.GetOtherGenresForCampaignCommission(campaignid);
        }
        #endregion
        #endregion
    }
}
