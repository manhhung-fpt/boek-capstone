using Boek.Infrastructure.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Boek.Infrastructure.ViewModels.Publishers
{
    public class PublisherViewModel
    {
        [Int]
        public int? Id { get; set; }
        [String]
        public string Code { get; set; }
        [String]
        public string Name { get; set; }
        [String]
        public string Email { get; set; }
        [String]
        public string Address { get; set; }
        [String]
        public string ImageUrl { get; set; }
        [String, Phone]
        public string Phone { get; set; }
    }
}
