using System.ComponentModel.DataAnnotations;

namespace CarRepairShop.web.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        [MinLength(1)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
