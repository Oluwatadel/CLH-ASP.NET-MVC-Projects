using MySchool.Core.Domain.Entities;
using System.Linq.Expressions;

namespace MySchool.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User entity);
        User Update(User entity);
        Task<User> GetAsync(string email);
        Task<User> GetAsync(Expression<Func<User, bool>> exp);
        Task<ICollection<User>> GetAllAsync();
        Task<bool> ExistAsync(string email);
    }
}
