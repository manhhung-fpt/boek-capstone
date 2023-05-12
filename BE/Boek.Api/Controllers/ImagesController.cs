using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Boek.Core.Constants;

namespace Boek.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ImagesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ImagesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Upload an image
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Upload(IFormFile file)
        {
            var cloudinary = new Cloudinary(new Account(
                cloud: _configuration.GetSection(MessageConstants.BOEK_CLOUDINARY_NAME).Value,
                apiKey: _configuration.GetSection(MessageConstants.BOEK_CLOUDINARY_API_KEY).Value,
                apiSecret: _configuration.GetSection(MessageConstants.BOEK_CLOUDINARY_API_SECRET).Value
            ));

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream())
            };
            var uploadResult = cloudinary.Upload(uploadParams);

            return Ok(new { uploadResult.Url });
        }
    }
}
