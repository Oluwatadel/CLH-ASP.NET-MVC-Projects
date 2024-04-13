using DMSMVC.Models.DTOs;
using DMSMVC.Models.Entities;

namespace DMSMVC.Service.Interface
{
    public interface IDepartmentService
    {
        Task<BaseResponse<DepartmentDTO>> CreateDepartmentAsync(DepartmentRequestModel request);
        Task<BaseResponse<DepartmentDTO>> GetDepartmentAsync(string exp);
        Task<BaseResponse<DepartmentDTO>> GetDepartmentByIdAsync(string id);
        Task<BaseResponse<ICollection<DepartmentDTO>>> GetAllDepartment();
		Task<BaseResponse<DepartmentDTO>> DeleteDepartment(string id);
        Task<BaseResponse<DepartmentDTO>> MakeAStaffHeadOfDepartment(string departmentName, string staffNumber);
		Task<BaseResponse<DepartmentDTO>> UpdateDepartment(string id, DepartmentUpdateModel request);
        //String DepartmentChoice(DepartmentEnum department);
    }
}
