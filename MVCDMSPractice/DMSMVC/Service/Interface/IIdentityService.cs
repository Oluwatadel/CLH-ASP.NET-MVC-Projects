using DMSMVC.Models.RequestModel;

namespace DMSMVC.Service.Interface
{
    public interface IIdentityService
	{
		Task<bool> IsCredentialsValid(LoginRequestModel loginRequest);
		Task<string> GetDepartment(string email);

	}
}
