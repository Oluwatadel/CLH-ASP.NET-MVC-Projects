using DMSMVC.Models.DTOs;
using DMSMVC.Models.Entities;
using DMSMVC.Models.RequestModel;

namespace DMSMVC.Service.Interface
{
    public interface IStaffService
    {
        //bool Register(string FirstName, string LastName, string PhoneNumber, GenderEnum gender, string staffNumber, int departmentId, string level, string position, string email, string password, string SecurityQuestion, string SecurityAnswer);
        Task<StaffDto> CreateAsync(string id, StaffDetailsModel staffDetailsModel);
        Task<bool> DeleteStaff(string email);
		Task<StaffDto> UpdateStaffAsync(string id, StaffDetailsModel staffDetailsModel);
        Task<StaffDto> GetStaffById(string id);
        Task<BaseResponse<ICollection<StaffDto>>> GetStaffs(string departmentId);
        Task<StaffDto> GetStaffByStaffNumber(string staffNumber);



    }
}
