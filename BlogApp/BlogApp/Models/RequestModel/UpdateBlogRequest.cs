using BlogApp.Entities;

namespace BlogApp.Models.RequestModel
{
    public class UpdateBlogRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
