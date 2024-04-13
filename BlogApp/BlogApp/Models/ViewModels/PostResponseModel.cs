namespace BlogApp.Models.ViewModels
{
    public class PostResponseModel
    {
        public bool IsSuccessfull { get; set; }
        public string Message { get; set; }
        public PostModel Data { get; set; }

    }
}
