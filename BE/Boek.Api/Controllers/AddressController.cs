using Microsoft.AspNetCore.Mvc;
using Boek.Core.Constants;
using Boek.Service.Commons;
using System.Net;

namespace Boek.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AddressController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region Get all provinces

        /// <summary>
        /// Get all provinces
        /// </summary>
        /// <returns></returns>

        #endregion

        [HttpGet("provinces")]
        public ActionResult<Province[]> GetProvinces()
        {
            return ProvincesList.PROVINCES;
        }

        #region Get all districts by province code

        /// <summary>
        /// Get all districts by province code
        /// </summary>
        /// <returns></returns>

        #endregion

        [HttpGet("provinces/{ProvinceCode}/districts")]
        public ActionResult<District[]> GetDistricts(int ProvinceCode)
        {
            var districts = DistrictsList.DISTRICTS.Where(d => d.ParentCode == ProvinceCode).ToArray();
            if (districts.Length == 0)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.ADDRESS_DISTRICT
                });
            }

            return districts;
        }

        #region Get all districts by province code

        /// <summary>
        /// Get all districts by province code
        /// </summary>
        /// <returns></returns>

        #endregion

        [HttpGet("districts/{DistrictCode}/wards")]
        public ActionResult<Ward[]> GetWards(int DistrictCode)
        {
            var wards = WardsList.WARDS.Where(w => w.ParentCode == DistrictCode).ToArray();
            if (wards.Length == 0)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.ADDRESS_WARDS
                });
            }

            return wards;
        }
    }
}