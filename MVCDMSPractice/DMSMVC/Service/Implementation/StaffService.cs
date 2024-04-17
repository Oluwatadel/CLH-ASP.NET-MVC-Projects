using DMSMVC.Models.DTOs;
using DMSMVC.Models.Entities;
using DMSMVC.Models.RequestModel;
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

        public async Task<bool> DeleteStaff(string id)
        {
            var staff = await _staffRepository.GetAsync(a => a.Id == id);
            var user = await _userRepository.GetAsync(a => a.Id == staff.UserId);
            _staffRepository.Delete(staff);
            _userRepository.Delete(user);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<ICollection<StaffDto>?> GetStaffs(string departmentID)
        {
            var staffs = await _staffRepository.GetAllAsync();
            return staffs != null ? staffs.Where(a => a.DepartmentId == departmentID).Select(p =>
            new StaffDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName =p.LastName,
                Gender = (GenderEnum)p.Gender!,
                Email = p.User.Email,
                DepartmentName = p.Department.DepartmentName,
                StaffNumber = p.StaffNumber,
                Level = p.Level,
                Role = p.Role,
                ImageUrl = p.ProfilePhotoUrl,
                phonenumber = p.PhoneNumber
            }).ToList() : null;
        }

        public async Task<StaffDto?> GetStaffById(string id)
        {
            var staff = await _staffRepository.GetAsync(p => p.Id == id);
            return staff != null ? new StaffDto
            {
                Id = staff.Id,
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                Gender = (GenderEnum)staff.Gender!,
                Email = staff.User.Email,
                DepartmentName = staff.Department.DepartmentName,
                StaffNumber = staff.StaffNumber,
                Level = staff.Level,
                Role = staff.Role,
                ImageUrl = staff.ProfilePhotoUrl,
                phonenumber = staff.PhoneNumber
            } : null;
        }

        public async Task<StaffDto?> UpdateStaffAsync(string id, StaffDetailsModel staffDetailsModel)
        {
            var staff = await _staffRepository.GetAsync(a => a.Id == id);
            var department = await _departmentRepository.GetAsync(a => a.DepartmentName == staffDetailsModel.DepartmentName);
            if (staff  == null) return null;
            staff.StaffNumber = staffDetailsModel.StaffNumber ?? staff.StaffNumber;
            staff.DepartmentId = department.Id;
            staff.Department = department;
            staff.Level = staffDetailsModel.Level ?? staff.Level;
            staff.FirstName = staffDetailsModel.FirstName ?? staff.FirstName;
            staff.LastName = staffDetailsModel.FirstName ?? staff.LastName;
            staff.User.Email = staffDetailsModel.Email ?? staff.User.Email;
            staff.Gender = staffDetailsModel.Gender;
            staff.ProfilePhotoUrl = _fileRepository.Upload(staffDetailsModel.ProfilePhotoUrl);
            var staffToBeUpdated = _staffRepository.Update(staff);
            await _unitOfWork.SaveAsync();
            return new StaffDto
            {
                Id = staff.Id,
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                Gender = (GenderEnum)staff.Gender!,
                Email = staff.User.Email,
                DepartmentName = staff.Department.DepartmentName,
                StaffNumber = staff.StaffNumber,
                Level = staff.Level,
                Role = staff.Role,
                ImageUrl = staff.ProfilePhotoUrl,
                phonenumber = staff.PhoneNumber
            };

        }

		public async Task<BaseResponse<StaffDto>> CreateAsync(string id, StaffDetailsModel staffDetailsModel)
		{
            var user = await _userRepository.GetAsync(a => a.Id == id);
            var staffExist = await _staffRepository.GetAsync(a => a.StaffNumber == staffDetailsModel.StaffNumber);
            if (staffExist != null)
            {
                return new BaseResponse<StaffDto>
                {
                    Message = "Staff Exist",
                    Status = false,
                };
            }
			//var department = await _departmentRepository.GetAsync(a => a.DepartmentName == staffDetailsModel.DepartmentName)!;
			var staffAdded = new Staff
			{
                UserId = user.Id,
				Level = staffDetailsModel.Level,
				StaffNumber = staffDetailsModel.StaffNumber!,
                FirstName = staffDetailsModel.FirstName,
                LastName = staffDetailsModel.LastName,
                Gender = staffDetailsModel.Gender,
                PhoneNumber = staffDetailsModel.PhoneNumber,
                ProfilePhotoUrl = _fileRepository.Upload(staffDetailsModel.ProfilePhotoUrl),
                User = user,
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
					FirstName = staffAdded.FirstName,
                    LastName = staffAdded.LastName,
                    Email = user.Email,
                    Gender = staffDetailsModel.Gender,
                    ImageUrl = staffAdded.ProfilePhotoUrl,
                    StaffNumber = staffAdded.StaffNumber,
                    DepartmentName = staffAdded.Department.DepartmentName,
                    Level = staffAdded.Level,
                    Role = staffAdded.Role,
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
                    FirstName = staff.FirstName,
                    LastName = staff.LastName,
                    Gender = (GenderEnum)staff.Gender!,
                    Email = staff.User.Email,
                    DepartmentName = staff.Department.DepartmentName,
                    StaffNumber = staff.StaffNumber,
                    Level = staff.Level,
                    Role = staff.Role,
                    ImageUrl = staff.ProfilePhotoUrl
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