using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.ViewModels.Addresses;
using Boek.Infrastructure.ViewModels.Users.Customers;
using Boek.Infrastructure.ViewModels.Users.Issuers;
using System.ComponentModel.DataAnnotations;

namespace Boek.Infrastructure.ViewModels.Users
{
    public class MultiUserViewModel
    {
        [Guid]
        public Guid? Id { get; set; }
        [String]
        public string Code { get; set; }
        [String]
        public string Name { get; set; }
        [String, EmailAddress]
        public string Email { get; set; }
        [String]
        public string Address { get; set; }
        [Child]
        public AddressViewModel AddressViewModel { get; set; }
        [String, Phone]
        public string Phone { get; set; }
        [String]
        public string RoleName { get; set; }
        [Byte]
        public byte? Role { get; set; }
        [Boolean]
        public bool? Status { get; set; }
        public string StatusName { get; set; }
        [String, Url]
        public string ImageUrl { get; set; }
        [DateRange]
        public DateTime? CreatedDate { get; set; }
        [DateRange]
        public DateTime? UpdatedDate { get; set; }

        public CustomerLevelViewModel Customer { get; set; }
        public BasicIssuerViewModel Issuer { get; set; }
    }
}
