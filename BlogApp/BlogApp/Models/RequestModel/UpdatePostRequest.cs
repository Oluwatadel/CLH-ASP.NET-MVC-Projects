using BlogApp.Entities;

namespace BlogApp.Models.RequestModel
{
    public class UpdatePostRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
