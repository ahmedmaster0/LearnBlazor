using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.ViewModels
{
    public class RegisterRequestDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email Required")]
        public string? Email { get; set; } 

        [Required(AllowEmptyStrings =false,ErrorMessage = "Password Required")]
        public string? Password { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage = "Confirm Password Required")]
        [Compare("Password", ErrorMessage = "Password dowsnt Match")]
        public string? ConfirmPassword { get; set; }

        [Required(AllowEmptyStrings =false ,ErrorMessage ="Role Required")]
        public string? RoleName { get; set; }
    }
}
