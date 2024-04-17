
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

    

    
}
