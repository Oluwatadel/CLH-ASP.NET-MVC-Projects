using DMSMVC.Models.DTOs;
using DMSMVC.Models.Entities;
using DMSMVC.Repository.Implementation;
using DMSMVC.Repository.Interface;
using DMSMVC.Service.Interface;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace DMSMVC.Service.Implementation
{
	public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
		private readonly UserManager<User> _userManager;

        public UserService(IUserRepository userRepository)
        {

            _userRepository = userRepository;
        }

      
        

  //      public async Task<BaseResponse<UserDTO>> LoginAsync(LoginRequest loginRequest)
  //      {

		//	var userExist = _userRepository.IsExist(loginRequest.Email);
		//	if (userExist)
		//	{
		//		var user = await _userRepository.GetAsync(a => a.Email == loginRequest.Email);
		//		if (loginRequest.Password != user.Password)
		//		{
		//			return new BaseResponse<UserDTO>
		//			{
		//				Status = false,
		//				Message = "Wrong Password",
		//				Data = null
		//			};
		//		}
		//		return new BaseResponse<UserDTO>
		//		{
		//			Status = true,
		//			Message = "Login Successfull",
		//			Data = new UserDTO
		//			{
		//				FullName = $"{user.LastName} {user.FirstName}",
		//				Email = user.Email,
		//				Gender = user.Gender.Value,
		//				PhoneNumber = user.PhoneNumber,
		//				ProfilePhotoUrl = user.ProfilePhotoUrl,
		//				StaffNumber = user.Staff.StaffNumber,
		//				Chat = user.Chat,
		//			}
		//		};
		//	}
		//	return new BaseResponse<UserDTO>
		//	{
		//		Status = false,
		//		Message = "Wrong Input or User does not not exist",
		//		Data = null
		//	};
		//}

		public async Task<BaseResponse<UserDTO>> CheckForAnswerToSecurityQuestion(string email)
		{
			var user = await _userRepository.GetAsync(a => a.Email == email);
			if (user == null)
			{
				return new BaseResponse<UserDTO>
				{
					Status = false,
					Message = "User doe not exist",
					Data = null
				};
			}
			else
			{
				return new BaseResponse<UserDTO>
				{
					Status = true,
					Data = new UserDTO
					{
						FullName = $"{user.LastName + user.FirstName}",
						StaffNumber = user.Staff.StaffNumber,
						Email = user.Email,
						Gender = user.Gender.Value,
						PhoneNumber = user.PhoneNumber,
						ProfilePhotoUrl = user.ProfilePhotoUrl
					},
					Message = $"Successfull"
				};
			}
		}

		public async Task<User> GetUserAsyn(string email)
		{
			return await _userRepository.GetAsync(a => a.Email == email);
		}

		public async Task<BaseResponse<UserDTO>> ReturnSecurityQuestionForgottenID(string email)
		{
			var user = await _userRepository.GetAsync(a => a.Email == email);
			if (user == null)
			{
				return new BaseResponse<UserDTO>
				{
					Status = false,
					Message = "User not found",
					Data = null

				};
			}
			return new BaseResponse<UserDTO>
			{
				Status = true,
				Message = "Successful",
				Data = new UserDTO
				{
					FullName = $"{user.LastName + user.FirstName}",
					StaffNumber = user.Staff.StaffNumber,
					Email = user.Email,
					Gender = user.Gender.Value,
					PhoneNumber = user.PhoneNumber,
					ProfilePhotoUrl = user.ProfilePhotoUrl
				}
			};
		}

		public async Task DeleteUser(User user)
		{
			_userRepository.Delete(user);
		}
	}
}
