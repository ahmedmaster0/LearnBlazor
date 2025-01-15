using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.ViewModels
{
    public class EditUserDTO
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="Email Required")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Invalid Email")]
        public string? Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Full Name Required")]
        public string? FullName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email Required")]
        [DataType(DataType.PhoneNumber,ErrorMessage = "Invalid Phone Number")]
        public string? Phone { get; set; }
    }
}
