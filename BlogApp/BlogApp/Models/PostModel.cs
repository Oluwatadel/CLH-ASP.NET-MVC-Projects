using BlogApp.Entities;

namespace BlogApp.Models
{
    public class PostModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string BlogTitle { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
