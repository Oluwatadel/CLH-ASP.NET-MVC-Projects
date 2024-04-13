using BlogApp.Data;
using BlogApp.Interfaces.Repositories;

namespace BlogApp.Implementations.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogDbContext _dbContext;
        public UnitOfWork(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Save()
        {
            return _dbContext.SaveChanges();
        }
    }
}
