using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.ViewModels
{
    public class LoginRequestDTO
    {
        [Required(AllowEmptyStrings =false,ErrorMessage ="Email Required")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Invalid Email")]
        public string? Email { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="Password Required")]
        public string? Password { get; set; }

    }
}
