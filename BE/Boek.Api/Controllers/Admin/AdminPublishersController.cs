using Boek.Infrastructure.Requests.Publishers;
using Boek.Infrastructure.ViewModels.Publishers;
using Boek.Service.Interfaces;
using Boek.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Boek.Core.Constants;

namespace Boek.Api.Controllers.Admin
{
    [Route("api/admin/publishers")]
    [ApiController]
    [Authorize(Roles = nameof(BoekRole.Admin))]
    public class AdminPublishersController : ControllerBase
    {
        private readonly IPublisherService _publisherService;
        public AdminPublishersController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        /// <summary>
        /// Create a publisher by admin
        /// </summary>
        /// <param name="createdPublisher"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_PUBLISHER })]
        public ActionResult<PublisherViewModel> CreatePublisher([FromBody] CreatePublisherRequestModel createdPublisher)
        {
            return Ok(_publisherService.CreatePublisher(createdPublisher));
        }

        /// <summary>
        /// Update a publisher by admin
        /// </summary>
        /// <param name="updatedPublisher"></param>
        /// <returns></returns>
        [HttpPut]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_PUBLISHER })]
        public ActionResult<PublisherViewModel> UpdatePublisher([FromBody] UpdatePublisherRequestModel updatedPublisher)
        {
            return _publisherService.UpdatePublisher(updatedPublisher);
        }

        /// <summary>
        /// Delete a publisher by admin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_PUBLISHER })]
        [HttpDelete("{id}")]
        public ActionResult<PublisherViewModel> DeleteAuthor(int id)
        {
            return _publisherService.DeletePublisher(id);
        }
    }
}
