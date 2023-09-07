using System.ComponentModel.DataAnnotations;

namespace HuniMVC.Models
{
    public class User
    {
        public Guid UserId { get; set; }

        public string Name { get; set; }
        public virtual string Email { get; set; }
        public string Password { get; set; }
        public string EmailConfirmed { get; set; }
        = "false";
        public string PasswordConfirmed { get; set; }
        = "true";


    }
}
