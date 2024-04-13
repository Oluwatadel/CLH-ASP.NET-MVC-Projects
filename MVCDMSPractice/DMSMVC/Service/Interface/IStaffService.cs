using DMSMVC.Models.DTOs;
using DMSMVC.Models.Entities;

namespace DMSMVC.Service.Interface
{
    public interface IStaffService
    {
        //bool Register(string FirstName, string LastName, string PhoneNumber, GenderEnum gender, string staffNumber, int departmentId, string level, string position, string email, string password, string SecurityQuestion, string SecurityAnswer);
        Task<BaseResponse<StaffDto>> CreateAsync(UserRequestModel userRequestModel);
        Task<bool> DeleteStaff(string email);
		Task<BaseResponse<StaffDto>> UpdateStaffAsync(string id, UserRequestModel userRequestModel);
        Task<BaseResponse<StaffDto>> GetStaffById(string id);
        Task<BaseResponse<ICollection<StaffDto>>> GetStaffs(string departmentId);
        Task<BaseResponse<StaffDto>> GetStaffByStaffNumber(string staffNumber);



    }
}
