using DMSMVC.Models.Entities;

namespace DMSMVC.Models.DTOs
{
    public class ChatDTO
    {
        public string Id { get; set; }
        public string SenderEmail { get; set; }
        public string ReceiverEmail { get; set; }
        public User User { get; set; }
        public ICollection<ChatContent> ChatContents { get; set; } = new HashSet<ChatContent>();
    }

    public class ChatRequestModel
    {
        public string? Id { get; set; }
        public string SenderEmail { get; set; }
        public string ReceiverEmail { get; set; }
        public string? Message { get; set; }
    }
}
