using System.ComponentModel.DataAnnotations;
using Boek.Core.Constants;

namespace Boek.Core.Validations
{
    public class BirthdayAttribute : ValidationAttribute
    {
        public int Age { get; set; }
        public BirthdayAttribute(int Age = 13)
        {
            this.Age = Age;
            ErrorMessage = ErrorMessageConstants.CUSTOMER_BIRTHDAY_REQUIREMENT + $" {Age} tuổi trở lên";
        }

        public override bool IsValid(object value)
        {
            if (value == null || Age <= 0)
                return false;
            DateTime date;
            if (DateTime.TryParse(value.ToString(), out date))
                return DateTime.Today.AddYears(-Age) >= date;
            return false;
        }
    }
}