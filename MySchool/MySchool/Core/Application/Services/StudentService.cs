using MySchool.Core.Application.Dtos;
using MySchool.Core.Application.Interfaces.Repositories;
using MySchool.Core.Application.Interfaces.Services;
using MySchool.Core.Domain.Entities;
using MySchool.Infrastructure.Persistence.Repositories;
using System.Linq.Expressions;
using System.Security.Claims;

namespace MySchool.Core.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IFileRepository _fileRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IGuardianRepository _guardianRepository;

        public StudentService(IStudentRepository studentRepository, IUserRepository userRepository, IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor, IFileRepository fileRepository, IRoleRepository roleRepository, IGuardianRepository guardianRepository)
        {
            _studentRepository = studentRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
            _fileRepository = fileRepository;
            _roleRepository = roleRepository;
            _guardianRepository = guardianRepository;
        }

        public async Task<BaseResponse<StudentDto>> CreateAsync(StudentRequest request)
        {
            var exists = await _userRepository.ExistAsync(request.Email);
            if (exists)
            {
                return new BaseResponse<StudentDto>
                {
                    Message = "Email already exists!!!",
                    Status = false,
                    Data = null
                };
            }

            if (request.Password != request.ConfirmPassword)
            {
                return new BaseResponse<StudentDto>
                {
                    Message = "Password does not match",
                    Status = false,
                    Data = null
                };
            }

            var user = new User
            {
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                PhoneNumber = request.PhoneNumber,
                Gender = request.Gender,
                ImageUrl = await _fileRepository.UploadAsync(request.ImageUrl)
            };

            var role = await _roleRepository.GetAsync("student");

            var userRole = new UserRole()
            {
                UserId = user.Id,
                User = user,
                RoleId = role.Id,
                Role = role,
            };

            user.UserRoles.Add(userRole);

            await _userRepository.CreateAsync(user);
            var currentUser = _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var guardian = await _guardianRepository.GetAsync(guar => guar.UserId == currentUser);

            var student = new Student()
            {
                UserId = user.Id,
                User = user,
                Guardian = guardian,
                GuardianId = guardian.Id,
                AdmissionNumber = $"CLH-00{new Random().Next(111, 999)}",
            };

            await _studentRepository.CreateAsync(student);
            await _unitOfWork.SaveAsync();

            return new BaseResponse<StudentDto>
            {
                Message = "Registration Successfull!!!",
                Status = true,
                Data = new StudentDto
                {
                    UserId = user.Id,
                    FullName = $"{user.FirstName} {user.LastName}",
                    GuardianId = guardian.Id,
                    GuardianName = $"{guardian.User.FirstName} {guardian.User.LastName}",
                    AdmissionNumber = student.AdmissionNumber
                },
            };
        }

        public async Task<BaseResponse<ICollection<StudentDto>>> GetAllAsync()
        {
            var students = await _studentRepository.GetAllAsync();
            return new BaseResponse<ICollection<StudentDto>>
            {
                Message = "Students found",
                Status = true,
                Data = students.Select(stu => new StudentDto
                {
                    UserId = stu.UserId,
                    FullName = $"{stu.User.FirstName} {stu.User.LastName}",
                    GuardianId = stu.GuardianId,
                    GuardianName = $"{stu.Guardian.User.FirstName} {stu.Guardian.User.LastName}",
                    AdmissionNumber = stu.AdmissionNumber
                }).ToList()
            };
        }

        public Task<BaseResponse<StudentDto>> GetAsync(string admisionNumber)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<ICollection<StudentDto>>> GetSelectedAsync(Expression<Func<Student, bool>> exp)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<StudentDto>> Update(string id, UpdateStudentRequest entity)
        {
            throw new NotImplementedException();
        }
    }
}
