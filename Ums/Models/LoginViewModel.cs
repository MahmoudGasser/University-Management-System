using System.ComponentModel.DataAnnotations;

namespace Ums.Models
{

    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ErrorMessage { get; set; }
        public bool RememberMe { get; set; } 

    }

}
