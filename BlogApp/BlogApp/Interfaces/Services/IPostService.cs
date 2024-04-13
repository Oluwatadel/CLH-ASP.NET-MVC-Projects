using BlogApp.Models;
using BlogApp.Models.RequestModel;
using BlogApp.Models.ViewModels;

namespace BlogApp.Interfaces.Services
{
    public interface IPostService
    {
        PostResponseModel CreatePost(PostRequest postRequest);
        BaseResponse UpdatePost(Guid id, UpdatePostRequest postRequest);
        BaseResponse DeletePost(Guid id);
        PostResponseModel GetPost(Guid id);
        PostsResponse GetPostsByBlog(Guid blogId);
    }
}
