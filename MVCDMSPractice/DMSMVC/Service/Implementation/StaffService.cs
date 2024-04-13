using DMSMVC.Models.DTOs;
using DMSMVC.Models.Entities;
using DMSMVC.Repository.Interface;
using DMSMVC.Service.Interface;

namespace DMSMVC.Service.Implementation
{
	public class StaffService : IStaffService
    {

        private readonly IStaffRepository _staffRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IFileRepository _fileRepository;
        private readonly IDepartmentRepository _departmentRepository;



        public StaffService(IStaffRepository staffRepository, IUnitOfWork unitOfWork, IDepartmentRepository departmentRepository, IFileRepository fileRepository, IUserRepository userRepository)
        {
            _staffRepository = staffRepository;
            _unitOfWork = unitOfWork;
            _fileRepository = fileRepository;
            _userRepository = userRepository;
            _departmentRepository = departmentRepository;

        }

        public async Task<bool> DeleteStaff(string email)
        {
            var staff = await _staffRepository.GetAsync(a => a.User.Email == email);
            var user = await _userRepository.GetAsync(a => a.Id == staff.UserId);
            _staffRepository.Delete(staff);
            _userRepository.Delete(user);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<BaseResponse<ICollection<StaffDto>>> GetStaffs(string departmentID)
        {
            var staffs = await _staffRepository.GetAllAsync();
            var staffsOfDepartment = staffs.Where(a => a.DepartmentId == departmentID).Select(p =>
            new StaffDto
            {
                Id = p.Id,
                FirstName = p.User.FirstName,
                LastName =p.User.LastName,
                Gender = (GenderEnum)p.User.Gender,
                Email = p.User.Email,
                DepartmentName = p.Department.DepartmentName,
                StaffNumber = p.StaffNumber,
                Level = p.Level,
                Position = p.Position,
                ImageUrl = p.User.ProfilePhotoUrl
            }).ToList();
            return new BaseResponse<ICollection<StaffDto>>
            {
                Status = true,
                Message = "Successfull",
                Data = staffsOfDepartment
            };
        }

        public async Task<BaseResponse<StaffDto>> GetStaffById(string id)
        {
            var staff = await _staffRepository.GetAsync(a => a.Id == id);
            return new BaseResponse<StaffDto>
            {
                Status = true,
                Message = $"Successfull",
                Data = new StaffDto
                {
					Id = staff.Id,
					FirstName = staff.User.FirstName,
                    LastName = staff.User.LastName,
                    Gender = (GenderEnum)staff.User.Gender,
                    Email = staff.User.Email,
					DepartmentName = staff.Department.DepartmentName,
					StaffNumber = staff.StaffNumber,
					Level = staff.Level,
					Position = staff.Position,
					ImageUrl = staff.User.ProfilePhotoUrl
				}
            };
        }

        public async Task<BaseResponse<StaffDto>> UpdateStaffAsync(string id, UserRequestModel userRequestModel)
        {
            var staff = await _staffRepository.GetAsync(a => a.Id == id);
            var department = await _departmentRepository.GetAsync(a => a.DepartmentName == userRequestModel.DepartmentName);
            staff.StaffNumber = userRequestModel.StaffNumber ?? staff.StaffNumber;
            staff.DepartmentId = department.Id;
            staff.Department = department;
            staff.Level = userRequestModel.Level ?? staff.Level;
            staff.User.FirstName = userRequestModel.FirstName ?? staff.User.FirstName;
            staff.User.LastName = userRequestModel.FirstName ?? staff.User.LastName;
            staff.User.Email = userRequestModel.Email ?? staff.User.Email;
            staff.User.Gender = userRequestModel.Gender;
            staff.User.ProfilePhotoUrl = _fileRepository.Upload(userRequestModel.ProfilePhotoUrl);
            var staffToBeUpdated = _staffRepository.Update(staff);
            await _unitOfWork.SaveAsync();
            return new BaseResponse<StaffDto>
            {
                Status = true,
                Message = "Update successful",
                Data = new StaffDto
                {
                    Id = staff.Id,
                    FirstName = staff.User.FirstName,
                    LastName = staff.User.LastName,
                    Gender = (GenderEnum)staff.User.Gender,
                    Email = staff.User.Email,
                    DepartmentName = staff.Department.DepartmentName,
                    StaffNumber = staff.StaffNumber,
                    Level = staff.Level,
                    Position = staff.Position,
                    ImageUrl = staff.User.ProfilePhotoUrl
                }
            };

        }

		public async Task<BaseResponse<StaffDto>> CreateAsync(UserRequestModel userRequestModel)
		{
            var userExist = _userRepository.IsExist(userRequestModel.Email);
            var staffExist = await _staffRepository.GetAsync(a => a.StaffNumber == userRequestModel.StaffNumber);
            if (staffExist != null || userExist)
            {
                return new BaseResponse<StaffDto>
                {
                    Message = "Password does not match",
                    Status = false,
                    Data = new StaffDto
                    {
                        Id = staffExist.Id,
                        StaffNumber = staffExist.StaffNumber,
                        DepartmentName = staffExist.Department.DepartmentName,
                        FirstName = staffExist.User.FirstName,
                        LastName = staffExist.User.LastName,
                        Email = staffExist.User.Email,
                        Gender = (GenderEnum)staffExist.User.Gender,
                        Level = staffExist.Level,
                        Position = staffExist.Position,
                        ImageUrl = staffExist.User.ProfilePhotoUrl
                    }
                };
            }
			var user = new User
			{
				Email = userRequestModel.Email,
				FirstName = userRequestModel.FirstName,
				LastName = userRequestModel.LastName,
				Gender = userRequestModel.Gender,
				PhoneNumber = userRequestModel.PhoneNumber,
				Password = userRequestModel.Password,
				SecurityQuestion = userRequestModel.SecurityQuestion,
				SecurityAnswer = userRequestModel.SecurityAnswer,
				ProfilePhotoUrl = _fileRepository.Upload(userRequestModel.ProfilePhotoUrl),
			};

			var department = await _departmentRepository.GetAsync(a => a.DepartmentName == userRequestModel.DepartmentName)!;
			var staffAdded = new Staff
			{
                UserId = user.Id,
				Department = department,
				DepartmentId = department.Id,
				Level = userRequestModel.Level,
				StaffNumber = userRequestModel.StaffNumber
			};
			await _userRepository.CreateAsync(user);
            await _staffRepository.CreateAsync(staffAdded);
			await _unitOfWork.SaveAsync();
			return new BaseResponse<StaffDto>
			{
				Message = "Registration Successfull!!!",
				Status = true,
				Data = new StaffDto
				{
                    Id = staffAdded.Id,
					FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Gender = userRequestModel.Gender,
                    ImageUrl = user.ProfilePhotoUrl,
                    StaffNumber = staffAdded.StaffNumber,
                    DepartmentName = staffAdded.Department.DepartmentName,
                    Level = staffAdded.Level,
                    Position = staffAdded.Position,
				}
			};

		}

        public async Task<BaseResponse<StaffDto>> GetStaffByStaffNumber(string staffNumberOrEmail)
        {
            var staff = await _staffRepository.GetAsync(a => (a.StaffNumber == staffNumberOrEmail) || (a.User.Email == staffNumberOrEmail));
            if(staff == null)
            {
                return new BaseResponse<StaffDto>
                {
                    Message = "User does not exist",
                    Status = false,
                    Data = null
                };
            }

            return new BaseResponse<StaffDto>
            {
                Status = true,
                Message = "Sucessfull",
                Data = new StaffDto
                {
                    Id = staff.Id,
                    FirstName = staff.User.FirstName,
                    LastName = staff.User.LastName,
                    Gender = (GenderEnum)staff.User.Gender,
                    Email = staff.User.Email,
                    DepartmentName = staff.Department.DepartmentName,
                    StaffNumber = staff.StaffNumber,
                    Level = staff.Level,
                    Position = staff.Position,
                    ImageUrl = staff.User.ProfilePhotoUrl
                }
            };
        }

		//public async Task<BaseResponse<StaffDto>> UpdateStaff(UserUpdateModel updateModel)
		//{
		//	var staff = await _staffRepository.GetAsync(p => p.StaffNumber == updateModel.StaffNumber);
		//	if (staff == null)
		//	{
		//		return new BaseResponse<StaffDto>
		//		{
		//			Status = false,
		//			Message = "Staff not found",
		//			Data = null
		//		};
		//	}
		//	staff.StaffNumber = updateModel.StaffNumber;
		//	staff.Level = updateModel.Level ?? staff.Level;
		//	staff.User.FirstName = updateModel.FirstName ?? staff.User.FirstName;
		//	staff.User.LastName = updateModel.LastName ?? staff.User.LastName;
		//	staff.Department = _departmentRepository.GetAsync(a => a.DepartmentName == updateModel.DepartmentName)
		//		.Result ?? staff.Department;

		//	staff.User.PhoneNumber = updateModel.PhoneNumber ?? staff.User.PhoneNumber;
		//	staff.User.Email = updateModel.Email ?? staff.User.Email;
		//	staff.User.Gender = updateModel.Gender;
		//	staff.User.ProfilePhotoUrl = _fileRepository.Upload(updateModel.ProfilePhotoUrl);
		//	_staffRepository.Update(staff);
		//	_unitOfWork.Save();
		//	return new BaseResponse<StaffDto>
		//	{
		//		Message = "Update Successfull",
		//		Status = true,
		//		Data = new StaffDto
		//		{
		//			FullName = $"{staff.User.LastName} {staff.User.FirstName}",
		//			StaffNumber = updateModel.StaffNumber,
		//			DepartmentName = staff.Department.DepartmentName,
		//			Level = staff.Level,
		//			Position = staff.Position
		//		},
		//	};
		//}
	}
}