using Microsoft.EntityFrameworkCore;
using MySchool.Core.Application.Interfaces.Repositories;
using MySchool.Core.Domain.Entities;
using MySchool.Infrastructure.Persistence.Context;
using System.Linq.Expressions;

namespace MySchool.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly StudContext _context;
        public UserRepository(StudContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User entity)
        {
            await _context.Set<User>().AddAsync(entity);
            return entity;
        }


        public async Task<bool> ExistAsync(string email)
        {
            return await _context.Set<User>().AnyAsync(x => x.Email == email);
        }

        public async Task<ICollection<User>> GetAllAsync()
        {
            return await _context.Set<User>().ToListAsync();
        }

        public async Task<User> GetAsync(string email)
        {
            var user = await _context.Set<User>().Include(a => a.UserRoles)
                .ThenInclude(a => a.Role).FirstOrDefaultAsync(a => a.Email == email);
            return user;
        }

        public async Task<User> GetAsync(Expression<Func<User, bool>> exp)
        {
            var user = await _context.Set<User>().Include(a => a.UserRoles)
                .ThenInclude(a => a.Role).FirstOrDefaultAsync(exp);
            return user;
        }

        public User Update(User entity)
        {
            var user = _context.Set<User>().Update(entity);
            return entity;
        }
    }
}
