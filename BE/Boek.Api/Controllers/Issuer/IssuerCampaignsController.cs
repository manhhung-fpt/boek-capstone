using Boek.Infrastructure.Requests.Campaigns;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.Campaigns;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Boek.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Boek.Core.Constants;

namespace Boek.Api.Controllers.Issuer
{
    [Route("api/issuer/campaigns")]
    [ApiController]
    [Authorize(Roles = nameof(BoekRole.Issuer))]
    public class IssuerCampaignsController : ControllerBase
    {
        #region Fields and constructor
        private readonly ICampaignService _campaignService;
        public IssuerCampaignsController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }
        #endregion

        #region Campaigns
        /// <summary>
        /// Get all campaigns by issuer
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_CAMPAIGN })]
        public ActionResult<BaseResponsePagingModel<CampaignViewModel>> GetCampaigns([FromQuery] CampaignRequestModel filter,
            [FromQuery] PagingModel paging)
        {
            return _campaignService.GetCampaignsByIssuer(filter, paging);
        }

        /// <summary>
        /// Get other campaigns by issuer
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet("other")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_CAMPAIGN })]
        public ActionResult<BaseResponsePagingModel<CampaignViewModel>> GetOtherCampaigns([FromQuery] CampaignRequestModel filter,
            [FromQuery] PagingModel paging)
        {
            return _campaignService.GetOtherCampaignsByIssuer(filter, paging);
        }

        /// <summary>
        /// Get a campaign by id by issuer
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_CAMPAIGN })]
        public ActionResult<CampaignViewModel> GetCampaignById(int id)
        {
            return _campaignService.GetCampaignByIdByIssuer(id);
        }
        /// <summary>
        /// Get other campaign by id by issuer
        /// </summary>
        [HttpGet("others/{id}")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_CAMPAIGN })]
        public ActionResult<CampaignViewModel> GetOtherCampaignById(int id)
        {
            return _campaignService.GetOtherCampaignByIdByIssuer(id);
        }
        #endregion

        #region Campaign Books
        /*/// <summary>
        /// Get all campaign books by issuer
        /// </summary>
        /// <param name="IssuerId"></param>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_CAMPAIGN })]
        [HttpGet("books")]
        public ActionResult<BaseResponsePagingModel<CampaignBookRespondModel>> GetCampaignBooks([FromQuery] CampaignBookRequestModel filter,
            [FromQuery] PagingModel paging)
        {
            return _campaignBookService.GetCampaignBooksByIssuer(filter, paging);
        }
        /// <summary>
        /// Get campaign book by id by issuer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="IssuerId"></param>
        /// <returns></returns>
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_CAMPAIGN })]
        [HttpGet("books/{id}")]
        public ActionResult<CampaignBookRespondModel> GetCampaignBookById(int id)
        {
            return _campaignBookService.GetCampaignBookByIdByIssuer(id);
        }
        /// <summary>
        /// Update a campaign book by issuer
        /// </summary>
        /// <param name="updatedCampaignBook"></param>
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_CAMPAIGN })]
        [HttpPut("books")]
        public ActionResult<CampaignBookRespondModel> UpdateCampaignBook([FromBody] UpdateCampaignBookRequestModel updatedCampaignBook)
        {
            return _campaignBookService.UpdateCampaignBook(updatedCampaignBook);
        }*/
        #endregion
    }
}
