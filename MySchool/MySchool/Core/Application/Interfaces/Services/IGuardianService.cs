using MySchool.Core.Application.Dtos;

namespace MySchool.Core.Application.Interfaces.Services
{
    public interface IGuardianService
    {
        Task<BaseResponse<GuardianDto>> CreateAsync(GuardianRequest request);
        Task<BaseResponse<GuardianDto>> Update(string id, UpdateGuardianRequest request);
        Task<BaseResponse<GuardianDto>> GetAsync(string id);
        Task<BaseResponse<ICollection<GuardianDto>>> GetAllAsync();
    }
}
