using BlogApp.Data;
using BlogApp.Entities;
using BlogApp.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Implementations.Repository
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogDbContext _dbContext;
        public BlogRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Blog Create(Blog blog)
        {
            _dbContext.Blogs.Add(blog);
            return blog;
        }

        public void Delete(Blog blog)
        {
            _dbContext.Blogs.Remove(blog);
        }

        public Blog Get(Guid id)
        {
            return _dbContext.Blogs.Find(id);
        }
        
        public Blog Get(string title)
        {
            return _dbContext.Blogs.SingleOrDefault(a => a.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        }

        public ICollection<Blog> GetAll()
        {
            return _dbContext.Blogs.ToList();
        }

        public bool IsExist(string title)
        {
            return _dbContext.Blogs.Any(b => b.Title == title);
        }

        public Blog Update(Blog blog)
        {
            _dbContext.Blogs.Update(blog);
            return blog;
        }
    }
}
