using System.ComponentModel.DataAnnotations;

namespace DMSMVC.Models.RequestModel
{
    public class UserUpdateRequest
    {
        public string Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
