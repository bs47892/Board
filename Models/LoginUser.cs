using System.ComponentModel.DataAnnotations;

namespace Board.Models
{
    public class LoginUser
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string UserEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [MinLength(8, ErrorMessage = "Password should be 8 or more characters")]
        public string UserPassword { get; set; }
    }
}
