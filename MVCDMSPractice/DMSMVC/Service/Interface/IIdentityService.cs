using DMSMVC.Models.DTOs;

namespace DMSMVC.Service.Interface
{
	public interface IIdentityService
	{
		Task<bool> IsCredentialsValid(LoginRequest loginRequest);
		Task<string> GetDepartment(string email);

	}
}
