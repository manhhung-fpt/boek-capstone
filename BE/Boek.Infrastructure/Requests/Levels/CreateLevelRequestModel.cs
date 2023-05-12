using System.ComponentModel.DataAnnotations;
using Boek.Core.Constants;

namespace Boek.Infrastructure.Requests.Levels
{
    public class CreateLevelRequestModel
    {
        [MaxLength(255)]
        [Required(ErrorMessage = $"{MessageConstants.MESSAGE_REQUIRED} {ErrorMessageConstants.LEVEL_NAME}")]
        public string Name { get; set; }
        [Required(ErrorMessage = $"{MessageConstants.MESSAGE_REQUIRED} {ErrorMessageConstants.LEVEL_CONDITIONAL_POINT}")]
        public int? ConditionalPoint { get; set; }
    }
}
