using Microsoft.EntityFrameworkCore;
using MySchool.Core.Application.Interfaces.Repositories;
using MySchool.Core.Domain.Entities;
using MySchool.Infrastructure.Persistence.Context;

namespace MySchool.Infrastructure.Persistence.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly StudContext _context;
        public ClassRepository(StudContext context) 
        {  
            _context = context; 
        }
        
        public async Task<Class> CreateAsync(Class entity)
        {
            await _context.Set<Class>().AddAsync(entity);
            return entity;
        }

        public async Task<bool> ExistAsync(string name)
        {
            return await _context.Classes.AnyAsync(a => a.Name == name);
        }

        public async Task<ICollection<Class>> GetAllAsync()
        {
            return await _context.Set<Class>().ToListAsync();
        }

        public async Task<Class> GetAsync(string name)
        {
            var clas = await _context.Set<Class>().Include(a => a.StudentClasses).ThenInclude(a => a.Student).Include(a => a.Teacher).ThenInclude(a => a.User).FirstOrDefaultAsync(a => a.Name == name);
            return clas;
        }

        public Class Update(Class entity)
        {
            var clas = _context.Set<Class>().Update(entity);
            return entity;
        }
    }
}
