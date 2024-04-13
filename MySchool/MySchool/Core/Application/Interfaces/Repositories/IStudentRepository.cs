using MySchool.Core.Domain.Entities;
using System.Linq.Expressions;

namespace MySchool.Core.Application.Interfaces.Repositories
{
    public interface IStudentRepository
    {
        Task<Student> CreateAsync(Student entity);
        Student Update(Student entity);
        Task<Student> GetAsync(string admisionNumber);
        Task<ICollection<Student>> GetSelectedAsync(Expression<Func<Student, bool>> exp);
        Task<ICollection<Student>> GetAllAsync();
        Task<bool> ExistAsync(string admissionNumber);
    }
}
