using Boek.Infrastructure.Requests.Authors;
using Boek.Infrastructure.ViewModels.Authors;
using Boek.Service.Interfaces;
using Boek.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Boek.Core.Constants;

namespace Boek.Api.Controllers.Admin
{
    [Route("api/admin/authors")]
    [ApiController]
    [Authorize(Roles = nameof(BoekRole.Admin))]
    public class AdminAuthorsController : ControllerBase
    {
        #region Field(s) and constructor
        private readonly IAuthorService _authorService;
        public AdminAuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        #endregion

        /// <summary>
        /// Create an author by admin
        /// </summary>
        /// <param name="createdAuthor"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_AUTHOR })]
        public ActionResult<AuthorViewModel> CreateAuthor([FromBody] CreateAuthorRequestModel createdAuthor)
        {
            return _authorService.CreateAuthor(createdAuthor);
        }

        /// <summary>
        /// Update an author by admin
        /// </summary>
        /// <param name="updatedAuthor"></param>
        /// <returns></returns>
        [HttpPut]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_AUTHOR })]
        public ActionResult<AuthorViewModel> UpdateAuthor([FromBody] UpdateAuthorRequestModel updatedAuthor)
        {
            return _authorService.UpdateAuthor(updatedAuthor);
        }

        /// <summary>
        /// Delete an author by admin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_AUTHOR })]
        [HttpDelete("{id}")]
        public ActionResult<AuthorViewModel> DeleteAuthor(int id)
        {
            return _authorService.DeleteAuthor(id);
        }
    }
}
