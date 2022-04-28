using System.ComponentModel.DataAnnotations;

namespace Northwind.Models
{
    public class CustomerWithPassword
    {
        public Customer Customer { get; set; }
        [UIHint("password"), Required]
        public string Password { get; set; }
    }
    public class LoginModel
    {
        [Required, UIHint("email")]
        public string Email { get; set; }

        [Required, UIHint("password")]
        public string Password { get; set; }
    }
    public class EmailLinkModel{
        [Required, UIHint("email")]
        public string Email { get; set; }

    }
    public class NewPasswordModel{
        [Required, UIHint("password")]
        public string Password { get; set; }
        [Required, UIHint("password")]
        public string PasswordAgain { get; set; }
    }
}