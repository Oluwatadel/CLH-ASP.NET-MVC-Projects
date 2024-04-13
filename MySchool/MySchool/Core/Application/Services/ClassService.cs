﻿using MySchool.Core.Application.Dtos;
using MySchool.Core.Application.Interfaces.Repositories;
using MySchool.Core.Application.Interfaces.Services;
using MySchool.Core.Domain.Entities;
using MySchool.Infrastructure.Persistence.Repositories;

namespace MySchool.Core.Application.Services
{
    public class ClassService : IClassService
    {
        public readonly IClassRepository _classRepository;
        public readonly IUnitOfWork _unitOfWork;

        public ClassService(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }
    
        public async Task<BaseResponse<ClassDto>> CreateAsync(ClassRequest request)
        {
            var classExist = await _classRepository.ExistAsync(request.Name);
            if (classExist)
            {
                return new BaseResponse<ClassDto>
                {
                    Message = "Class already exist",
                    Status = false,
                    Data = null
                };
            }

            var classCreated = new Class()
            {
                Name = request.Name,
            };
            await _unitOfWork.SaveAsync();

            return new BaseResponse<ClassDto>
            {
                Message = "Successfull",
                Status = true,
                Data = new ClassDto
                {
                    Name = request.Name,
                }
            };
        }

        public async Task<BaseResponse<ICollection<ClassDto>>> GetAllAsync()
        {
            var classes = await _classRepository.GetAllAsync();
            var classesDto = classes.Select(a => new ClassDto
            {
                Name = a.Name,
                TeacherFullName = a.Teacher.User.FirstName + " " + a.Teacher.User.LastName,
                Id = a.Id,
                Students = a.StudentClasses.Select(a => new StudentDto
                {
                    UserId = a.StudentId,
                    FullName = $"{a.Student.User.FirstName} {a.Student.User.LastName}",
                    AdmissionNumber = a.Student.AdmissionNumber,
                }).ToList(),
            }).ToList();

            return new BaseResponse<ICollection<ClassDto>>
            {
                Message = "Successfully Found",
                Status = true,
                Data = classesDto,
            };
        }

        public async Task<BaseResponse<ClassDto>> GetAsync(string name)
        {
            var classGotten = await _classRepository.GetAsync(name);
            return new BaseResponse<ClassDto>
            {
                Message = "Class found",
                Status = true,
                Data = new ClassDto
                {
                    Name = classGotten.Name,
                    TeacherFullName = $"{classGotten.Teacher.User.FirstName} {classGotten.Teacher.User.LastName}",
                    Students = classGotten.StudentClasses.Select(x => new StudentDto
                    {
                        FullName = $"{x.Student.User.FirstName} {x.Student.User.FirstName}",
                        GuardianId = x.Student.GuardianId,
                        GuardianName = x.Student.Guardian.User.FirstName + " " + x.Student.Guardian.User.LastName,
                        AdmissionNumber = x.Student.AdmissionNumber,

                    }).ToList()
                }
            };
        }

        public  async Task<BaseResponse<ClassDto>> Update(string name, UpdateClassRequest request)
        {
            var classGotten = await  _classRepository.GetAsync(name);
            if (classGotten == null)
            {
                return new BaseResponse<ClassDto>
                {
                    Message = "Update Unsuccessfull",
                    Status = false,
                    Data = null,
                };
            }
            classGotten.Name = request.Name;
            _classRepository.Update(classGotten);
            await _unitOfWork.SaveAsync();
            return new BaseResponse<ClassDto>
            {
                Message = "Update successfull",
                Status = true,
                Data = new ClassDto
                {
                    Name = classGotten.Name,
                    TeacherFullName = $"{classGotten.Teacher.User.LastName} {classGotten.Teacher.User.FirstName}",
                    Students = classGotten.StudentClasses.Select(x => new StudentDto
                    {
                        FullName = $"{x.Student.User.FirstName} {x.Student.User.LastName}",
                        GuardianId = x.Student.GuardianId,
                        GuardianName = $"{x.Student.Guardian.User.FirstName} {x.Student.Guardian.User.LastName}",
                        AdmissionNumber = x.Student.AdmissionNumber,
                        UserId = x.Student.UserId,
                    }).ToList(),
                }
            };


        }
    }
}
