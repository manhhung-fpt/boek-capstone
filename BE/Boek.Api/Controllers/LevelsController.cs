using Boek.Infrastructure.Requests.Levels;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.Levels;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Boek.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LevelsController : ControllerBase
    {
        #region Field(s) and constructor
        private readonly ILevelService _levelService;

        public LevelsController(ILevelService levelService)
        {
            _levelService = levelService;
        }
        #endregion

        #region Gets
        /// <summary>
        /// Get all levels
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <param name="WithCustomers"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<BaseResponsePagingModel<LevelViewModel>> GetLevels([FromQuery] LevelRequestModel filter,
            [FromQuery] PagingModel paging, [FromQuery] bool WithCustomers = false)
        {
            return _levelService.GetLevels(filter, paging, WithCustomers);
        }

        /// <summary>
        /// Get a level by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="WithCustomers"></param>
        [HttpGet("{id}")]
        public ActionResult<LevelViewModel> GetLevelById(int id, [FromQuery] bool WithCustomers = false)
        {
            return _levelService.GetLevelById(id, WithCustomers);
        }
        #endregion
    }
}
