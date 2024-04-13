using BlogApp.Data;
using BlogApp.Entities;
using BlogApp.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Implementations.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogDbContext _dbContext;
        public PostRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Post Create(Post post)
        {
            _dbContext.Posts.Add(post);
            return post;
        }

        public void Delete(Post post)
        {
            _dbContext.Posts.Remove(post);
        }

        public Post Get(Guid id)
        {
            return _dbContext.Posts
                .Include(p => p.Blog)
                .SingleOrDefault(p => p.Id == id);
        }

        public ICollection<Post> GetAllBlogPosts(Guid blogId)
        {
            return _dbContext.Posts.Include(p => p.Blog).ToList();
        }

        public Post Update(Post post)
        {
            _dbContext.Posts.Update(post);
            return post;
        }

        public Post Get(string title)
        {
            return _dbContext.Posts
                .Include(p => p.Blog)
                .FirstOrDefault(x => x.Title.ToLower() == title.ToLower());  
           // return _dbContext.Posts.FirstOrDefault(x => x.Title.Equals(title, StringComparison.OrdinalIgnoreCase));  
        }
    }
}
