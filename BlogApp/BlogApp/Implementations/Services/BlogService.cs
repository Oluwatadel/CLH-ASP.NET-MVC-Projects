using BlogApp.Entities;
using BlogApp.Interfaces.Repositories;
using BlogApp.Interfaces.Services;
using BlogApp.Models;
using BlogApp.Models.RequestModel;
using BlogApp.Models.ViewModels;

namespace BlogApp.Implementations.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IUnitOfWork _unitOfWork;
        public BlogService(IBlogRepository blogRepository, IUnitOfWork unitOfWork)
        {
            _blogRepository = blogRepository;
            _unitOfWork = unitOfWork;
        }     

        public BlogResponseModel Add(BlogResquest resquest)
        {
            if (_blogRepository.IsExist(resquest.Title))
            {
                return new BlogResponseModel
                {
                    Message = "Blog already exists!!!",
                    IsSuccessfull = false,
                };
            }

            var blog = new Blog
            {
                Id = Guid.NewGuid(),
                Title = resquest.Title,
                Description = resquest.Description ?? $"It is about {resquest.Title}"
            };
            _blogRepository.Create(blog);
            _unitOfWork.Save();
            return new BlogResponseModel
            {
                Message = "Blog Creation Successfull!!!",
                IsSuccessfull = true,
                Data = new BlogModel
                {
                    Id = blog.Id,
                    Title = blog.Title,
                    Description = blog.Description,
                }
            };
        }

        public BaseResponse Delete(Guid id)
        {
            var blog = _blogRepository.Get(id);
            if (blog == null)
            {
                return new BaseResponse
                {
                    Message = $"Blog Doesn't Exist!!!",
                    IsSuccessful = false
                };
            }
            _blogRepository.Delete(blog);
            _unitOfWork.Save();
                return new BaseResponse
                {
                    Message = $"Blog Deleted Successful!!!",
                    IsSuccessful = true
                }; ;
        }

        public BlogResponseModel Get(Guid id)
        {
            var blog = _blogRepository.Get(id);
            if(blog == null)
            {
                return new BlogResponseModel
                {
                    Message = "Blog not found!!!",
                    IsSuccessfull = false
                };
            }
            return new BlogResponseModel
            {
                Message = "Blog Found Successfully!!!",
                IsSuccessfull = true,
                Data = new Models.BlogModel
                {
                    Id = blog.Id,
                    Title = blog.Title,
                    Description = blog.Description,
                }
            };
        }

        public BlogsResponse GetAll()
        {
            var blogs = _blogRepository.GetAll();
            return new BlogsResponse
            {
                Message = "Blogs list",
                IsSuccessfull = true,
                Data = blogs.Select(b => new BlogModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                }).ToList()
            };
        }

        public BaseResponse Update(Guid id, UpdateBlogRequest req)
        {
            var blog = _blogRepository.Get(id);
            if (blog == null)
            {
                return new BaseResponse
                {
                    Message = "Update Unsuccessfull!!!",
                    IsSuccessful = false
                };
            }

            var titleExist = _blogRepository.Get(req.Title);
            if(titleExist.Title == req.Title && titleExist.Id != id)
            {
                return new BaseResponse
                {
                    Message = $"Blog With Title {req.Title} Already Exist!!!",
                    IsSuccessful = false
                };
            }
            blog.Title = req.Title ?? blog.Title; //null coalison
            blog.Description = req.Description ?? blog.Description;
            _unitOfWork.Save();

            return new BaseResponse
            {
                Message = "Update Successfull",
                IsSuccessful = true,
            };
        }
    }
}
