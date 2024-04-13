using MySchool.Core.Domain.Entities;
using System.Linq.Expressions;

namespace MySchool.Core.Application.Interfaces.Repositories
{
    public interface IGuardianRepository
    {
        Task<Guardian> CreateAsync(Guardian entity);
        Guardian Update(Guardian entity);
        Task<Guardian> GetAsync(Expression<Func<Guardian, bool>> exp);
        Task<ICollection<Guardian>> GetAllAsync();
    }
}
