using MySchool.Core.Application.Interfaces.Repositories;
using MySchool.Infrastructure.Persistence.Context;

namespace MySchool.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StudContext _context;
        public UnitOfWork(StudContext context)
        {
            _context = context;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
