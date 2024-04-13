namespace BlogApp.Models.ViewModels
{
    public class BlogResponseModel
    {
        public bool IsSuccessfull { get; set; }
        public string Message { get; set; }
        public BlogModel Data { get; set; }
    }
}
