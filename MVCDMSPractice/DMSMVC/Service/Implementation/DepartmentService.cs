using DMSMVC.Models.DTOs;
using DMSMVC.Models.Entities;
using DMSMVC.Repository.Implementation;
using DMSMVC.Repository.Interface;
using DMSMVC.Service.Interface;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Collections.ObjectModel;

namespace DMSMVC.Service.Implementation
{
	public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStaffRepository _staffRepository;
        public DepartmentService(IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork, IStaffRepository staffRepository)
        {
            _departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _staffRepository = staffRepository;
        }
        public async Task<BaseResponse<DepartmentDTO>> CreateDepartmentAsync(DepartmentRequestModel request)
        {
            var department = await _departmentRepository.GetAsync(d => 
            d.DepartmentName == request.DepartmentName || d.Acronym == request.Acronym);
            if (department != null)
            {
                return new BaseResponse<DepartmentDTO>
                {
                    Status = false,
                    Message = "Department already exist",
                    Data = null
                };
            }
            var newDepartment = new Department
            {
                DepartmentName = request.DepartmentName!,
                Acronym = request.Acronym!,
            };
            await _departmentRepository.CreateAsync(newDepartment);
            await _unitOfWork.SaveAsync();
            return new BaseResponse<DepartmentDTO>
            {
                Status = true,
                Message = "Department created successfully",
                Data = new DepartmentDTO
                {
                    DepartmentName = newDepartment.DepartmentName,
                    Acronym = newDepartment.Acronym,
                }
            };

        }

        public async Task<BaseResponse<DepartmentDTO>> DeleteDepartment(string id)
        {
            var department = await _departmentRepository.GetAsync(a => a.Id == id);
            if (department == null)
            {
                return new BaseResponse<DepartmentDTO>
                {
                    Status = false,
                    Message = "Department cannot be found",
                    Data = null
                };
            }
            _departmentRepository.Delete(department);
            await _unitOfWork.SaveAsync();
            return new BaseResponse<DepartmentDTO>
            {
                Status = true,
                Message = "Department deleted successfully",
                Data = null
            };
        }

        public async Task<BaseResponse<DepartmentDTO>> GetDepartmentAsync(string exp)
        {
            var department = await _departmentRepository.GetAsync(a => a.DepartmentName == exp);
            if(department != null)
            {
                return new BaseResponse<DepartmentDTO>
                {
                    Status = true,
                    Message = "Department found",
                    Data = new DepartmentDTO
                    {
                        Id = department.Id,
                        DepartmentName = department.DepartmentName,
                        Acronym = department.Acronym,
                        Staffs = department.Staffs,
                        Documents = department.Documents,
                        HeadOfDepartmentStaffNumber = department.HeadOfDepartmentStaffNumber,
                    }
                };
            }
            return new BaseResponse<DepartmentDTO>
            {
                Status = false,
                Message = "Department cannot be found",
                Data = null
            };

        }

        public async Task<BaseResponse<DepartmentDTO>> GetDepartmentByIdAsync(string exp)
        {
            var department = await _departmentRepository.GetAsync(a => a.Id == exp);
            if (department != null)
            {
                return new BaseResponse<DepartmentDTO>
                {
                    Status = true,
                    Message = "Department found",
                    Data = new DepartmentDTO
                    {
                        Id = department.Id,
                        DepartmentName = department.DepartmentName,
                        Acronym = department.Acronym,
                        Staffs = department.Staffs,
                        Documents = department.Documents,
                        HeadOfDepartmentStaffNumber = department.HeadOfDepartmentStaffNumber,
                    }
                };
            }
            return new BaseResponse<DepartmentDTO>
            {
                Status = false,
                Message = "Department cannot be found",
                Data = null
            };
        }

        public async Task<BaseResponse<DepartmentDTO>> MakeAStaffHeadOfDepartment(string departmentName, string staffNumber)
        {
            var department = await _departmentRepository.GetAsync(a => a.DepartmentName == departmentName);
            if(department == null)
            {
                return new BaseResponse<DepartmentDTO>
                {
                    Status = false,
                    Message = "Department cannot be found",
                    Data = null
                };
            }

            //Get the previous HOD
            var previousHodStaffNumber = department.HeadOfDepartmentStaffNumber;
            var previousHOD = await _staffRepository.GetAsync(a => a.StaffNumber == previousHodStaffNumber);

            //Set previousHod's position to staff
            previousHOD.Position = "Staff";

            //set new HOD
			department.HeadOfDepartmentStaffNumber = staffNumber;

            //Get the Staff entity of the new HOD using his staffNumber
            var newHOD = department.Staffs
                .SingleOrDefault(a => a.StaffNumber == staffNumber);
			newHOD!.Position = "Director";
			await _unitOfWork.SaveAsync();


			return new BaseResponse<DepartmentDTO>
            {
                Status = true,
                Message = $"Mr {newHOD.User.LastName} {newHOD.User.FirstName} is now the director of {department.DepartmentName}",
                Data = new DepartmentDTO
                {
                    Id = department.Id,
                    DepartmentName = department.DepartmentName,
                    Acronym = department.Acronym,
                    Staffs = department.Staffs,
                    Documents= department.Documents,
                    HeadOfDepartmentStaffNumber = department.HeadOfDepartmentStaffNumber
                }
            };
        }

        public async Task<BaseResponse<DepartmentDTO>> UpdateDepartment(string id, DepartmentUpdateModel request)
        {
            var department = await _departmentRepository.GetAsync(a => a.Id == id);
            if (department == null)
            {
                return new BaseResponse<DepartmentDTO>
                {
                    Status = false,
                    Message = "Department not found",
                    Data = null
                };
            }
            department.DepartmentName = request.DepartmentName ?? department.DepartmentName;
            department.Acronym = request.Acronym ?? department.Acronym;
            department.HeadOfDepartmentStaffNumber = request.StaffNumberOfPotentialHOD;
            await _unitOfWork.SaveAsync();
            return new BaseResponse<DepartmentDTO>
            {
                Status = true,
                Message = "Department found",
                Data = new DepartmentDTO
                {
                    Id = department.Id,
                    Staffs = department.Staffs,
                    DepartmentName = department.DepartmentName,
                    HeadOfDepartmentStaffNumber = department.HeadOfDepartmentStaffNumber,
                    Acronym = department.Acronym,
                }
            };
        }

       public async Task<BaseResponse<ICollection<DepartmentDTO>>> GetAllDepartment()
        {
            var departments = await _departmentRepository.GetAllAsync();
			var departmentDTOs = departments
			   .Select(p => new DepartmentDTO
               {
                   Id = p.Id,
                   DepartmentName = p.DepartmentName,
                   Acronym = p.Acronym,
                   Documents = p.Documents,
                   HeadOfDepartmentStaffNumber = p.HeadOfDepartmentStaffNumber,
                   Staffs = p.Staffs
               }).ToList();
            return new BaseResponse<ICollection<DepartmentDTO>>
            {
                Status = true,
                Message = "Successfull",
                Data = departmentDTOs
			};
        }

        
    }
}