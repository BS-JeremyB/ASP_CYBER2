using System.ComponentModel.DataAnnotations;

namespace PresentationSiteWeb.Models
{
    public class UserLoginForm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
