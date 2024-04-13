using MySchool.Core.Application.Dtos;
using MySchool.Core.Domain.Entities;
using System.Linq.Expressions;

namespace MySchool.Core.Application.Interfaces.Services
{
    public interface IStudentService
    {
        Task<BaseResponse<StudentDto>> CreateAsync(StudentRequest request);
        Task<BaseResponse<StudentDto>> Update(string id, UpdateStudentRequest entity);
        Task<BaseResponse<StudentDto>> GetAsync(string admisionNumber);
        Task<BaseResponse<ICollection<StudentDto>>> GetSelectedAsync(Expression<Func<Student, bool>> exp);
        Task<BaseResponse<ICollection<StudentDto>>> GetAllAsync();
    }
}
