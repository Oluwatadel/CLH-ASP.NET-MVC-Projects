namespace DMSMVC.Models.Entities
{
    public class User : Base
    {
        //public string UserId { get; set; } = Guid.NewGuid().ToString().Substring(0,7);
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public GenderEnum? Gender { get; set; }
        public string Email { get; set; }
        public string? ProfilePhotoUrl { get; set; }
        public string Password { get; set; }
        public string? SecurityQuestion { get; set; }
        public string? SecurityAnswer { get; set; }
        public Staff? Staff { get; set; }

    }
}
