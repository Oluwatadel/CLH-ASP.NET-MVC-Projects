using Microsoft.EntityFrameworkCore;
using MySchool.Core.Application.Interfaces.Repositories;
using MySchool.Core.Domain.Entities;
using MySchool.Infrastructure.Persistence.Context;
using System.Linq.Expressions;

namespace MySchool.Infrastructure.Persistence.Repositories
{
    public class GuardianRepository : IGuardianRepository
    {
        private readonly StudContext _context;
        public GuardianRepository(StudContext context)
        {
            _context = context;
        }
       
        public async Task<Guardian> CreateAsync(Guardian entity)
        {
            await _context.Set<Guardian>().AddAsync(entity);
            return entity;
        }

        public async Task<ICollection<Guardian>> GetAllAsync()
        {
            return await _context.Set<Guardian>()
                .Include(a => a.Students)
                .Include(a => a.User)
                .ToListAsync();
        }

        public async Task<Guardian> GetAsync(Expression<Func<Guardian, bool>> exp)
        {
            var guardian = await _context.Set<Guardian>()
                .Include(a => a.Students)
                .Include(a => a.User)
                .FirstOrDefaultAsync(exp);
            return guardian;
        }

        public Guardian Update(Guardian entity)
        {
            var guardian = _context.Set<Guardian>().Update(entity);
            return entity;
        }
    }
}
