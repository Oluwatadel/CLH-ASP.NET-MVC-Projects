using BlogApp.Entities;
using BlogApp.Interfaces.Repositories;
using BlogApp.Interfaces.Services;
using BlogApp.Models;
using BlogApp.Models.RequestModel;
using BlogApp.Models.ViewModels;

namespace BlogApp.Implementations.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IBlogRepository _blogRepository;
        private readonly IUnitOfWork _unitOfWork;
        public PostService(IPostRepository postRepository, IBlogRepository blogRepository, IUnitOfWork unitOfWork)
        {
            _postRepository = postRepository;
            _blogRepository = blogRepository;
            _unitOfWork = unitOfWork;
        }
        public PostResponseModel CreatePost(PostRequest postRequest)
        {
            var blogExist = _blogRepository.Get(postRequest.BlogId);
            if(blogExist == null)
            {
                return new PostResponseModel
                {
                    Message = "Blog Not Found!!!",
                    IsSuccessfull = false
                };
            }
            var newPost = _postRepository.Get(postRequest.Title); 
            if(newPost != null)
            {
                return new PostResponseModel
                {
                    Message = "Post Title Already Exist!!!",
                    IsSuccessfull = false
                };
            }
            var post = new Post
            {
                Id = Guid.NewGuid(),
                Title = postRequest.Title,
                Content = postRequest.Content,
                BlogId = blogExist.Id
            };
            _postRepository.Create(post);
            _unitOfWork.Save();
            return new PostResponseModel
            {
                Message = "Post Created Successfully!!!",
                IsSuccessfull = true,
                Data = new PostModel
                {
                    Id = post.Id,
                    Title = postRequest.Title,
                    Content = postRequest.Content,
                    BlogTitle = blogExist.Title
                }
            };
            
        }

        public BaseResponse DeletePost(Guid id)
        {
            var post = _postRepository.Get(id);
            if(post == null)
            {
                return new BaseResponse
                {
                    Message = "Post Not Found!!!",
                    IsSuccessful = false
                };
            }
            _postRepository.Delete(post);
            _unitOfWork.Save();
            return new BaseResponse
            {
                Message = "Post Deleted Successfully",
                IsSuccessful = true
            };
        }

        public PostResponseModel GetPost(Guid id)
        {
            var post = _postRepository.Get(id);
            if( post == null )
            {
                return new PostResponseModel
                {
                    Message = "Post Not Found!!!",
                    IsSuccessfull = false
                };
            }
            return new PostResponseModel
            {
                Message = "Post Found!!!",
                IsSuccessfull = true,
                Data = new PostModel
                {
                    Id = post.Id,
                    BlogTitle = post.Title,
                    Content = post.Content,
                    Title = post.Title
                }
            };
        }

        public PostsResponse GetPostsByBlog(Guid blogId)
        {
            var posts = _postRepository.GetAllBlogPosts(blogId);
            if(posts == null )
            {
                return new PostsResponse
                {
                    Message = "Posts Not Found!!!",
                    IsSuccessfull = false
                };
            }
            return new PostsResponse
            {
                Message = "Posts Found!!!",
                IsSuccessfull = true,
                Data = posts.Select(post => new PostModel
                {
                    Id = post.Id,
                    BlogTitle = post.Blog.Title,
                    Content = post.Content,
                    Title = post.Title
                }).ToList()
            };
        }

        public BaseResponse UpdatePost(Guid id, UpdatePostRequest postRequest)
        {
            var post = _postRepository.Get(id);
            if(post == null )
            {
                return new BaseResponse
                {
                    Message = $"Post not Found!!!",
                    IsSuccessful = false
                };
            }

            var titleExist = _postRepository.Get(postRequest.Title);
            if(titleExist != null)
            {
                return new BaseResponse
                {
                    Message = $"Post With Title {postRequest.Title} Already Exist!!!",
                    IsSuccessful = false
                };
            }
            post.Title = postRequest.Title ?? post.Title;
            post.Content = postRequest.Content ?? post.Content;
            _postRepository.Update(post);
            _unitOfWork.Save();
            return new BaseResponse
            {
                Message = $"Post Updated!!!",
                IsSuccessful = true
            };
        }
    }
}
