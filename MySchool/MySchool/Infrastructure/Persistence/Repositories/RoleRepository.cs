using Microsoft.EntityFrameworkCore;
using MySchool.Core.Application.Interfaces.Repositories;
using MySchool.Core.Domain.Entities;
using MySchool.Infrastructure.Persistence.Context;
using System.Linq.Expressions;

namespace MySchool.Infrastructure.Persistence.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly StudContext _context;
        public RoleRepository(StudContext context)
        {
            _context = context;
        }
        
        public async Task<Role> CreateAsync(Role entity)
        {
            await _context.Set<Role>().AddAsync(entity);
            return entity;
        }

        public async Task<bool> ExistAsync(string name)
        {
            return await _context.Roles.AnyAsync(a => a.Name == name);
        }

        public async Task<ICollection<Role>> GetAllAsync()
        {
            return await _context.Set<Role>()
                .Include(a => a.UserRoles)
                .ThenInclude(a => a.User)
                .ToListAsync();
        }

        public async Task<Role> GetAsync(string name)
        {
            var role = await _context.Set<Role>()
                 .Include(a => a.UserRoles)
                 .ThenInclude(a => a.User)
                 .FirstOrDefaultAsync(a => a.Name == name);
            return role;
        }

        public async Task<Role> GetAsync(Expression<Func<Role, bool>> exp)
        {
            var role = await _context.Set<Role>()
                 .Include(a => a.UserRoles)
                 .ThenInclude(a => a.User)
                 .FirstOrDefaultAsync(exp);
            return role;
        }

        public Role Update(Role entity)
        {
            var role = _context.Set<Role>().Update(entity);
            return entity;
        }
    }
}
