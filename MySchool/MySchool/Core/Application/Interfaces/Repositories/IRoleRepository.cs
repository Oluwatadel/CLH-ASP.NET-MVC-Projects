using MySchool.Core.Domain.Entities;
using System.Linq.Expressions;

namespace MySchool.Core.Application.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<Role> CreateAsync(Role entity);
        Role Update(Role entity);
        Task<Role> GetAsync(string name);
        Task<ICollection<Role>> GetAllAsync();
        Task<Role> GetAsync(Expression<Func<Role, bool>> exp);
        Task<bool> ExistAsync(string name);
    }
}
