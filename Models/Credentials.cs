using System.ComponentModel.DataAnnotations;

namespace TravelNow.Models
{
    public class Credentials
    {
        [Required(ErrorMessage = "Email address required.")]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Password required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}