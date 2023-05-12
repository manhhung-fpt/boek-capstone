using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.Requests.Groups;
public class GroupRequestModel
    {
        [Int]
        public int? Id { get; set; }

        [String]
        public string Name { get; set; }

        [String]
        public string Description { get; set; }

        [Boolean]
        public bool? Status { get; set; }
    }