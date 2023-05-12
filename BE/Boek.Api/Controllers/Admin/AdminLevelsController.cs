using Boek.Core.Constants;
using Boek.Infrastructure.Requests.Levels;
using Boek.Infrastructure.ViewModels.Levels;
using Boek.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace Boek.Api.Controllers.Admin
{
    [Route("api/admin/levels")]
    [ApiController]
    public class AdminLevelsController : ControllerBase
    {
        #region Field(s) and constructor
        private readonly ILevelService _levelService;

        public AdminLevelsController(ILevelService levelService)
        {
            _levelService = levelService;
        }
        #endregion

        #region Create, Update, and Convert
        /// <summary>
        /// Create a level by admin
        /// </summary>
        /// <param name="createLevel"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_LEVEL })]
        public ActionResult<LevelViewModel> CreateLevel([FromBody] CreateLevelRequestModel createLevel)
        {
            return _levelService.CreateLevel(createLevel);
        }

        /// <summary>
        /// Update a level by admin
        /// </summary>
        /// <param name="updateLevel"></param>
        /// <returns></returns>
        [HttpPut]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_LEVEL })]
        public ActionResult<LevelViewModel> UpdateLevel([FromBody] UpdateLevelRequestModel updateLevel)
        {
            return _levelService.UpdateLevel(updateLevel);
        }

        /// <summary>
        /// Convert customers' level into a new level by admin
        /// </summary>
        /// <param name="NewLevelId"></param>
        /// <param name="OldLevelId"></param>
        /// <param name="DisableOldLevelStatus"></param>
        /// <returns></returns>
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_LEVEL })]
        [HttpPatch]
        public ActionResult<LevelViewModel> ConvertCustomerLevelByNewLevel([BindRequired] int OldLevelId, [BindRequired] int NewLevelId, [FromQuery] bool DisableOldLevelStatus = false)
        {
            return _levelService.ConvertCustomerLevelByNewLevel(OldLevelId, NewLevelId, DisableOldLevelStatus);
        }
        #endregion
    }
}
