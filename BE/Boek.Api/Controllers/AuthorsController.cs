using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.Authors;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Boek.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        #region Field(s) and constructor
        private readonly IAuthorService _authorService;
        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        #endregion

        #region Gets
        /// <summary>
        /// Get all authors
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <param name="WithBooks"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<BaseResponsePagingModel<AuthorBooksViewModel>> GetAuthors([FromQuery] AuthorViewModel filter,
            [FromQuery] PagingModel paging, [FromQuery] bool WithBooks = false)
        {
            return _authorService.GetAuthors(filter, paging, WithBooks);
        }

        /// <summary>
        /// Get an author by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="WithBooks"></param>
        [HttpGet("{id}")]
        public ActionResult<AuthorBooksViewModel> GetAuthorById(int id, [FromQuery] bool WithBooks = false)
        {
            return _authorService.GetAuthorById(id, WithBooks);
        }
        #endregion
    }
}
