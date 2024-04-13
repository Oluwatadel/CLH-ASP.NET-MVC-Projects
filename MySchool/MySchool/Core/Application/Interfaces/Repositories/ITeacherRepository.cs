using MySchool.Core.Domain.Entities;
using System.Linq.Expressions;

namespace MySchool.Core.Application.Interfaces.Repositories
{
    public interface ITeacherRepository
    {
        Task<Teacher> CreateAsync(Teacher entity);
        Teacher Update(Teacher entity);
        Task<Teacher> GetAsync(string staffNumber);
        Task<Teacher> GetAsync(Expression<Func<Teacher, bool>> exp);
        Task<ICollection<Teacher>> GetAllAsync();
        Task<bool> ExistAsync(string staffNumber);
    }
}
