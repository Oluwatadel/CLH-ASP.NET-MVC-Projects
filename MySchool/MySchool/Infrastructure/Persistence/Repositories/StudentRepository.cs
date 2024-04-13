using Microsoft.EntityFrameworkCore;
using MySchool.Core.Application.Interfaces.Repositories;
using MySchool.Core.Domain.Entities;
using MySchool.Infrastructure.Persistence.Context;
using System.Linq.Expressions;

namespace MySchool.Infrastructure.Persistence.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudContext _context;
        public StudentRepository(StudContext context)
        {
            _context = context;
        }
        
        public async Task<Student> CreateAsync(Student entity)
        {
            await _context.Set<Student>().AddAsync(entity);
            return entity;
        }

        public async Task<bool> ExistAsync(string admissionNumber)
        {
            return await _context.Students.AnyAsync(a => a.AdmissionNumber == admissionNumber);
        }

        public async Task<ICollection<Student>> GetAllAsync()
        {
            return await _context.Set<Student>().ToListAsync();
        }

        public async Task<Student> GetAsync(string admisionNumber)
        {
            var student = await _context.Set<Student>().Include(a => a.StudentClasses).
                ThenInclude(a => a.Class).FirstOrDefaultAsync(a => a.AdmissionNumber == admisionNumber);
            return student;
        }

        public async Task<ICollection<Student>> GetSelectedAsync(Expression<Func<Student, bool>> exp)
        {
            var selectedStudent = await _context.Set<Student>().Where(exp).ToListAsync();
            return selectedStudent;
        }

        public Student Update(Student entity)
        {
            var stud = _context.Set<Student>().Update(entity);
            return entity;
        }
    }
}
