using System.ComponentModel.DataAnnotations;
using Boek.Core.Constants;

namespace Boek.Infrastructure.Requests.Orders.Update
{
    public class UpdateAvailableOrderRequestModel
    {
        [Required(ErrorMessage = $"{MessageConstants.MESSAGE_REQUIRED} {ErrorMessageConstants.ORDER_ID}")]
        public Guid? Id { get; set; }
        [Required(ErrorMessage = $"{MessageConstants.MESSAGE_REQUIRED} {ErrorMessageConstants.ADDRESS}")]
        public string Address { get; set; }
        public string Note { get; set; }
    }
}