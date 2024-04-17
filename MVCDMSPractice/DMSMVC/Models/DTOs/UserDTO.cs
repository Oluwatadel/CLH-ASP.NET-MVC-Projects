using DMSMVC.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace DMSMVC.Models.DTOs
{
    public class UserDTO
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
