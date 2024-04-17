using DMSMVC.Models.DTOs;
using DMSMVC.Models.Entities;
using DMSMVC.Models.RequestModel;
using System.Linq.Expressions;

namespace DMSMVC.Service.Interface
{
    public interface IUserService
    {
        //Task<BaseResponse<UserDTO>> LoginAsync(LoginRequest loginRequest);
		Task<string> ReturnPassWord(string email);
		Task<string> ReturnSecurityQuestionForgottenID(string email);
		Task<UserDTO?> GetUserAsyn(string email);
		Task<UserDTO?> UpdateEmailPassword(string id, UserUpdateRequest userUpdateRequest);
        Task<UserDTO?> CreateUser(UserRegisterModel request);
        Task DeleteUser(string id);



    }
}
