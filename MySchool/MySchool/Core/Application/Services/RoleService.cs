using MySchool.Core.Application.Dtos;
using MySchool.Core.Application.Interfaces.Repositories;
using MySchool.Core.Application.Interfaces.Services;
using MySchool.Core.Domain.Entities;
using System.Data;

namespace MySchool.Core.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RoleService(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<BaseResponse<RoleDto>> CreateAsync(RoleRequest request)
        {
            var exists = await _roleRepository.GetAsync(request.Name);
            if (exists != null)
            {
                return new BaseResponse<RoleDto>
                {
                    Message = "Role already exists!!!",
                    Status = false,
                    Data = null,
                };
            }

            var role = new Role()
            {
                Name = request.Name,
                Description = request.Description
            };
            await _roleRepository.CreateAsync(role);
            await _unitOfWork.SaveAsync();

            var roleDto = new RoleDto()
            {
                Name = role.Name,
                Id = role.Id,

            };
            return new BaseResponse<RoleDto>
            {
                Message = "Role Creation Successfull",
                Status = true,
                Data = roleDto,
            };
        }

        public async Task<BaseResponse<ICollection<RoleDto>>> GetAllAsync()
        {
            var roles = await _roleRepository.GetAllAsync();
            return new BaseResponse<ICollection<RoleDto>>
            {
                Message = "Successfull",
                Status = true,
                Data = roles.Select(role => new RoleDto
                {
                    Id = role.Id,
                    Name = role.Name,
                    Users = role.UserRoles.Select(a => new UserDto()
                    {
                        FullName = $"{a.User.FirstName} {a.User.LastName}",
                        Age = DateTime.Now.Year - a.User.DateOfBirth.Year,
                    }).ToList(),
                }).ToList(),
            };
        }

        public async Task<BaseResponse<RoleDto>> GetAsync(string name)
        {
            var role = await _roleRepository.GetAsync(name);
            if (role == null)
            {
                return new BaseResponse<RoleDto>
                {
                    Message = "Role does not exist",
                    Status = false,
                    Data = null,
                };
            }
            var roleDto = new RoleDto()
            {
                Id = role.Id,
                Name = role.Name,
                Users = role.UserRoles.Select(a => new UserDto()
                {
                    FullName = $"{a.User.FirstName} {a.User.LastName}",
                    Age = DateTime.Now.Year - a.User.DateOfBirth.Year,
                }).ToList(),
            };

            return new BaseResponse<RoleDto>
            {
                Message = "Role Found Successfully",
                Status = true,
                Data = roleDto,
            };
        }

        public async Task<BaseResponse<RoleDto>> Update(string id, UpdateRoleRequest request)
        {
            var role = await _roleRepository.GetAsync(a => a.Id == id);
            if (role == null)
            {
                return new BaseResponse<RoleDto>
                {
                    Message = "Update Unsuccessfull",
                    Status = false,
                    Data = null,
                };
            }

            role.Description = request.Description;
            _roleRepository.Update(role);
            await _unitOfWork.SaveAsync();
            return new BaseResponse<RoleDto>
            {
                Message = "Update Successfull",
                Status = true,
                Data = new RoleDto
                {
                    Id = id,
                    Name = role.Name,
                    Users = role.UserRoles.Select(a => new UserDto
                    {
                        Id = a.User.Id,
                        FullName = a.User.FirstName + " " + a.User.LastName,
                        Age = DateTime.Now.Year - a.User.DateOfBirth.Year,
                    }).ToList(),
                },
            };
        }
    }
}
