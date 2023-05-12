using System.ComponentModel.DataAnnotations;

namespace Boek.Infrastructure.Requests.Groups;
public class CreateGroupRequestModel
{
    [MaxLength(255)]
    public string Name { get; set; }
    public string Description { get; set; }
}