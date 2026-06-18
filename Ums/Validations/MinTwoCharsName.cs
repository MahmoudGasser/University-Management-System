using System.ComponentModel.DataAnnotations;

namespace Ums.Validations
{
    public class MinTwoCharsName : ValidationAttribute
    {
        public MinTwoCharsName()
        {
            ErrorMessage = "Name must be at least 2 characters long.";
        }

        public override bool IsValid(object value)
        {
            var str = value.ToString();
            if (value == "")
                return false;
            if (string.IsNullOrWhiteSpace(str))
                return false;

            return (str.Trim().Length) >= 2;
        }

    }
}
