using BlogApp.Entities;

namespace BlogApp.Models.RequestModel
{
    public class PostRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid BlogId { get; set; }
    }



}
