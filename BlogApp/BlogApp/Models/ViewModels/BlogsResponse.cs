namespace BlogApp.Models.ViewModels
{
    public class BlogsResponse
    {
        public bool IsSuccessfull { get; set; }
        public string Message { get; set; }
        public ICollection<BlogModel> Data { get; set; }
    }
}
