namespace BlogApp.Models.ViewModels
{
    public class PostsResponse
    {
        public bool IsSuccessfull { get; set; }
        public string Message { get; set; }
        public ICollection<PostModel> Data { get; set; }
    }
}
