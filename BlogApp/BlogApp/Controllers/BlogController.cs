using BlogApp.Interfaces.Services;
using BlogApp.Models.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    
    public class BlogController : Controller
    {
        private readonly IPostService _postService;
        private readonly IBlogService _blogService;

        public BlogController(IPostService postService, IBlogService blogService)
        {
            _postService = postService;
            _blogService = blogService;
        }

        public IActionResult AddBlog()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddBlog(BlogResquest request)
        {
            var response = _blogService.Add(request);
            if(response.IsSuccessfull)
            {
                TempData["SuccessMessage"] = response.Message;
                return RedirectToAction("Index");
            }
            //This is to ensure if the above is not successfull the page sgould be re rendered with the request
            TempData["ErrorMessage"] = response.Message;
            return View(request);
            
        }

        public IActionResult Index() 
        { 
            var blogs = _blogService.GetAll();
            return View(blogs);
        }

        public IActionResult UpdateBlog(Guid id)
        {
            var blog = _blogService.Get(id);
            return View(blog);
        }

        [HttpPost]
        public IActionResult UpdateBlog(Guid id, UpdateBlogRequest request)
        {
            var update = _blogService.Update(id, request);
            if(update.IsSuccessful)
            {
                return RedirectToAction("Index");
            }
            return View(request);
        }

        public IActionResult DeleteBlog(Guid id)
        {
            var blog = _blogService.Get(id);
            return View(blog);
        }

        [HttpPost, ActionName("DeleteBlog")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var blog = _blogService.Delete(id);
            if(blog.IsSuccessful)
            {
                return RedirectToAction("Index");
            }

            return View(blog);
        }
    }
}
