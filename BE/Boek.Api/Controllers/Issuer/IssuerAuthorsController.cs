using Boek.Infrastructure.Requests.Authors;
using Boek.Infrastructure.ViewModels.Authors;
using Boek.Service.Interfaces;
using Boek.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Boek.Core.Constants;

namespace Boek.Api.Controllers.Issuer
{
    [Route("api/issuer/authors")]
    [ApiController]
    [Authorize(Roles = nameof(BoekRole.Issuer))]
    public class IssuerAuthorsController : ControllerBase
    {
        #region Field and constructor
        private readonly IAuthorService _authorService;
        public IssuerAuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        #endregion

        /// <summary>
        /// Create an author by issuer
        /// </summary>
        /// <param name="createdAuthor"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_AUTHOR })]
        public ActionResult<AuthorViewModel> CreateAuthor([FromBody] CreateAuthorByIssuerRequestModel createdAuthor)
        {
            return _authorService.CreateAuthorByIssuer(createdAuthor);
        }
    }
}
