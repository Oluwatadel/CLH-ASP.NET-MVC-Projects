using BlogApp.Models;
using BlogApp.Models.RequestModel;
using BlogApp.Models.ViewModels;

namespace BlogApp.Interfaces.Services
{
    public interface IBlogService
    {
        BlogResponseModel Add(BlogResquest resquest);
        BlogResponseModel Get(Guid id);
        BlogsResponse GetAll();
        BaseResponse Delete(Guid id);
        BaseResponse Update(Guid id, UpdateBlogRequest req);
    }
}
