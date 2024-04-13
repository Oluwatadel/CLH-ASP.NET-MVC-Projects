using MySchool.Core.Application.Dtos;

namespace MySchool.Core.Application.Interfaces.Services
{
    public interface ITeacherService
    {
        Task<BaseResponse<TeacherDto>> CreateAsync(GuardianRequest request);
        Task<BaseResponse<TeacherDto>> Update(string id, UpdateRoleRequest request);
        Task<BaseResponse<TeacherDto>> GetAsync(string name);
        Task<BaseResponse<ICollection<TeacherDto>>> GetAllAsync();
    }
}
