using BlogApp.Interfaces.Services;
using BlogApp.Models.RequestModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogApp.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IBlogService _blogService;

        public PostController(IPostService postService, IBlogService blogService)
        {
            _postService = postService;
            _blogService = blogService;
        }

        public IActionResult AddPost()
        {
            var blogs = _blogService.GetAll();
            ViewBag.Blogs = new SelectList(blogs.Data, "Id", "Title");
            return View();
        }

        [HttpPost]
        public IActionResult AddPost(PostRequest request)
        {
            var post = _postService.CreatePost(request);
            if(post.IsSuccessfull)
            {
                return RedirectToAction("Index", );
            }
            return View();
        }

        public IActionResult ViewPosts(Guid blogId)
        {
            var posts = _postService.GetPostsByBlog(blogId);
            return View(posts);
        }
    }
}
