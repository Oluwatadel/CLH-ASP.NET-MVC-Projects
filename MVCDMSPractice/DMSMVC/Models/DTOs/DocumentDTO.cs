
using DMSMVC.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace DMSMVC.Models.DTOs
{
    public class DocumentDTO : Base
    {
        //public string? DocumentId { get; set; }
        public String? Id { get; set; }
        public string? Title { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime TimeUploaded { get; set; } = DateTime.Now;
        public String DocumentUrl { get; set; } = default!;
        public string? Author { get; set; } = default!;
        public string? DepartmentName { get; set; } = default!;
    }

    public class DocumentRequestModel
    {
        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }
        [Required]
        public IFormFile file { get; set; }
        public string? Author { get; set; }
        public string? Password { get; set; }
        public bool IsDeleted { get; set; } = default;
    }

    public class UpdateModel
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public IFormFile DocumentUrl { get; set; } = default!;
    }
}
