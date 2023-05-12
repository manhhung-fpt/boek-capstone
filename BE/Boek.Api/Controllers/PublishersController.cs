using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.Publishers;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Boek.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        #region Field(s) and constructor
        private readonly IPublisherService _publisherService;
        public PublishersController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }
        #endregion

        #region Gets
        /// <summary>
        /// Get all publishers
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<BaseResponsePagingModel<PublisherViewModel>> GetPublishers([FromQuery] PublisherViewModel filter,
            [FromQuery] PagingModel paging)
        {
            return _publisherService.GetPublishers(filter, paging);
        }

        /// <summary>
        /// Get a publisher by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<PublisherViewModel> GetPublisherById(int id)
        {
            return _publisherService.GetPublisherById(id);
        }
        #endregion
    }
}
