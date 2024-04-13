using DMSMVC.Models.DTOs;
using DMSMVC.Models.Entities;
using System.Linq.Expressions;

namespace DMSMVC.Service.Interface
{
    public interface IUserService
    {
        //Task<BaseResponse<UserDTO>> LoginAsync(LoginRequest loginRequest);
		Task<BaseResponse<UserDTO>> CheckForAnswerToSecurityQuestion(string email);
		Task<BaseResponse<UserDTO>> ReturnSecurityQuestionForgottenID(string email);
		Task<User> GetUserAsyn(string email);


	}
}
