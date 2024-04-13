using BlogApp.Entities;
using BlogApp.Implementations.Repository;

namespace BlogApp.Interfaces.Repositories
{
    public interface IBlogRepository
    {
        Blog Create(Blog blog);
        Blog Update (Blog blog);
        void Delete (Blog blog);
        Blog Get(Guid id);
        Blog Get(string title);
        ICollection<Blog> GetAll();
        bool IsExist(string title);
    }
}
