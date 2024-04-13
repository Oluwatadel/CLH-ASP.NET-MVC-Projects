namespace BlogApp.Entities
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid BlogId { get; set; }
        public Blog Blog { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
