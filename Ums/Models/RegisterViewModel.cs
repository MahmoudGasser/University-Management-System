using System.ComponentModel.DataAnnotations;

namespace Ums.Models
{
    public class RegisterViewModel
    {

        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
