using MySchool.Core.Application.Dtos;

namespace MySchool.Core.Application.Interfaces.Services
{
    public interface IClassService
    {
        Task<BaseResponse<ClassDto>> CreateAsync(ClassRequest request);
        Task<BaseResponse<ClassDto>> Update(string name, UpdateClassRequest request);
        Task<BaseResponse<ClassDto>> GetAsync(string name);
        Task<BaseResponse<ICollection<ClassDto>>> GetAllAsync();
    }
}
