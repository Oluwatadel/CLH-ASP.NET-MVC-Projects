using Microsoft.EntityFrameworkCore;
using MySchool.Core.Application.Interfaces.Repositories;
using MySchool.Core.Domain.Entities;
using MySchool.Infrastructure.Persistence.Context;
using System.Linq.Expressions;

namespace MySchool.Infrastructure.Persistence.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly StudContext _context;
        public TeacherRepository(StudContext context)
        {
            _context = context;
        }

        public async Task<Teacher> CreateAsync(Teacher entity)
        {
            await _context.Set<Teacher>().AddAsync(entity);
            return entity;
        }

        public async Task<bool> ExistAsync(string staffNumber)
        {
            return await _context.Teachers.AnyAsync(a => a.StaffNumber == staffNumber);
        }

        public async Task<ICollection<Teacher>> GetAllAsync()
        {
            return await _context.Set<Teacher>().ToListAsync();
        }

        public async Task<Teacher> GetAsync(string staffNumber)
        {
            var teacherGotten = await _context.Set<Teacher>().Include(a => a.Class)
                .ThenInclude(a => a.StudentClasses).FirstOrDefaultAsync(a => a.StaffNumber == staffNumber);
            return teacherGotten;
        }

        public async Task<Teacher> GetAsync(Expression<Func<Teacher, bool>> exp)
        {
            var teacherGotten = await _context.Set<Teacher>().Include(a => a.Class)
                 .ThenInclude(a => a.StudentClasses).FirstOrDefaultAsync(exp);
            return teacherGotten;
        }

        public Teacher Update(Teacher entity)
        {
            var teacher = _context.Set<Teacher>().Update(entity);
            return entity;
        }
    }
}
