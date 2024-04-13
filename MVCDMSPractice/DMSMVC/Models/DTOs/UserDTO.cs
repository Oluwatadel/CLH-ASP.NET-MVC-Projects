using DMSMVC.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace DMSMVC.Models.DTOs
{
    public class UserDTO
    {
        public string? Id { get; set; }
        public string FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public GenderEnum Gender { get; set; }
        public string? Email { get; set; }
        public string? StaffNumber { get; set; }
        public string? ProfilePhotoUrl { get; set; }
		public string? SecurityQuestion { get; set; }
		public string? SecurityAnswer { get; set; }
		public ICollection<Chat> Chat { get; set; }
    }
    public class UserUpdateModel
    {
        [Required(ErrorMessage = "Enter the first name")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Enter the Last name")]
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public GenderEnum Gender { get; set; }
        public string? Email { get; set; }
        public String? StaffNumber { get; set; }
        public IFormFile ProfilePhotoUrl { get; set; } = default!;
        public string Level { get; set; }
        public string DepartmentName { get; set; }

    }
    
    public class UserRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public GenderEnum Gender { get; set; }
        public string Email { get; set; }
        public IFormFile ProfilePhotoUrl { get; set; }
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
        public string StaffNumber { get; set; } = default!;
        public string Level { get; set; }
        public string? DepartmentName { get; set; } = default!;

    }

    public class LoginRequest
    {
        [Required(ErrorMessage = "Email Address is required")]
        public string Email { get; set; } = default!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;
    }

    public class ChangePasswordRequest
    {
        public string Password { get; set; } = default!;
        public string NewPassword { get; set; } = default!;
        public string ConfirmPassword { get; set; } = default!;
    }
}
