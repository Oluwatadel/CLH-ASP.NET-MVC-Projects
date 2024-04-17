using DMSMVC.Models.DTOs;
using DMSMVC.Models.RequestModel;
using DMSMVC.Repository.Interface;
using DMSMVC.Service.Interface;

namespace DMSMVC.Service.Implementation
{
	public class IdentityService : IIdentityService
	{
		private readonly IUserRepository _userRepository;

		public IdentityService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<string> GetDepartment(string email)
		{
			var user = await _userRepository.GetAsync(a => a.Email == email);
			return user.Staff.Department.DepartmentName;
		}

		public async Task<bool> IsCredentialsValid(LoginRequestModel loginRequest)
		{
			var user = await _userRepository.GetAsync(a => a.Email == loginRequest.Email);
			if(user != null)
			{
				if(user.Password == loginRequest.Password)
				{
					return true;
				}
				return false;
			}
			return false;
		}
	}
}
