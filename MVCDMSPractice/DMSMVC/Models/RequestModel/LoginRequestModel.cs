using System.ComponentModel.DataAnnotations;

namespace DMSMVC.Models.RequestModel
{
    public class LoginRequestModel
    {
        [Required(ErrorMessage = "Email Address is required")]
        public string Email { get; set; } = default!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;
    }
}
