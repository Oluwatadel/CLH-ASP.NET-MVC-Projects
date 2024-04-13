using MySchool.Core.Application.Dtos;

namespace MySchool.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<BaseResponse<UserDto>> LoginAsync(LoginRequest request);
        Task<BaseResponse<UserDto>> GetAsync(string id);
        Task<BaseResponse<ICollection<UserDto>>> GetUsers();
    }
}
