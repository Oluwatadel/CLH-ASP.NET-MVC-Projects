namespace DMSMVC.Models.RequestModel
{
    public class ChatContentRequest
    {
        public String? Id { get; set; }
        public string ChatContentId { get; set; } = Guid.NewGuid().ToString();
        public string? ContentOfChat { get; set; }
    }
}
