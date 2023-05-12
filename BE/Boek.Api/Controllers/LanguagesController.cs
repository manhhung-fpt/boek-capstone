using Microsoft.AspNetCore.Mvc;
using Boek.Core.Constants;
namespace Boek.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LanguagesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region Gets
        /// <summary>
        /// Get all languages
        /// </summary>
        /// <returns></returns>
        #endregion
        [HttpGet]
        public ActionResult<string[]> GetLanguages()
        {
            return Languages.LANGUAGES;
        }
    }
}