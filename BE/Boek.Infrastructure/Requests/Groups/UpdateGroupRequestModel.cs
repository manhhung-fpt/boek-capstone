using System.ComponentModel.DataAnnotations;

namespace Boek.Infrastructure.Requests.Groups;
public class UpdateGroupRequestModel
{
    public int? Id { get; set; }
    [MaxLength(255)]
    public string Name { get; set; }
    public string Description { get; set; }
    public bool? Status { get; set; }
}