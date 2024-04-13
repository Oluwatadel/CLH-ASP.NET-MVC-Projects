using DMSMVC.Models.DTOs;
using DMSMVC.Models.Entities;
using DMSMVC.Models.ViewModels;
using DMSMVC.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DMSMVC.Controllers
{
    public class DepartmentController : Controller
    {
        public readonly IUserService _userService;
        public readonly IStaffService _staffService;
        public readonly IDepartmentService _departmentService;

        public DepartmentController(IUserService userService, IStaffService staffService, IDepartmentService departmentService)
        {
            _userService = userService;
            _staffService = staffService;
            _departmentService = departmentService;
        }


        public async Task<IActionResult> ManageDepartment()
        {
            var departments =await  _departmentService.GetAllDepartment();
            ICollection<DepartmentViewModel> departmentViews = new List<DepartmentViewModel>();
            foreach(var department in departments.Data)
            {
                var departmentView = (new DepartmentViewModel
                {
                    Acronym = department.Acronym,
                    DepartmentName = department.DepartmentName,
                    Id = department.Id,
                    Documents = department.Documents,
                    Staffs = department.Staffs,
                });

                //Get the full name of the Director
                if (department.HeadOfDepartmentStaffNumber != null)
                {
					var hod = await _staffService.GetStaffByStaffNumber(department.HeadOfDepartmentStaffNumber);
                    departmentView.NameOfHod = $"{hod.Data.LastName} {hod.Data.FirstName}";
                }
				departmentViews.Add(departmentView);
			}
            return View(departmentViews);
        }

        public async Task<IActionResult> CreateDepartment() 
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateDepartment(DepartmentRequestModel requestModel) 
        {
            var response = await _departmentService.CreateDepartmentAsync(requestModel);
            if (response.Message == "Department already exist")return View(requestModel);
			TempData["Message"] = response.Message;
            return RedirectToAction("ManageDepartment");

        }

        public async Task<IActionResult> Delete(string id) 
        {
            var response = await _departmentService.GetDepartmentByIdAsync(id);
            if (response.Data == null) return BadRequest();
            //TempData["Message"] = response.Message;
            return  View(response.Data);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteDepartment(string id)
        {
            var response = await _departmentService.DeleteDepartment(id);
            TempData["Message"] = response.Message;
            return RedirectToAction("ManageDepartment");
                
        }

        public async Task<IActionResult> UpdateDepartment(string id)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(id);
            if (department.Data == null) return RedirectToAction("ManageDepartment");
            return View(department.Data);
            
        }

        [HttpPost, ActionName("UpdateDepartment")]
        public async Task<IActionResult> Update(string id, DepartmentUpdateModel updateModel)
        {
            var department = await _departmentService.UpdateDepartment(id, updateModel);
			return RedirectToAction("ManageDepartment");

        }

        public async Task<IActionResult> DisplayAllStaffsOfDepartment(string id)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(id);
            if (department.Data == null)
            {
                return View();
            }
            var staffs = new BaseResponse<ICollection<StaffDto>>
            {
                Status = true,
                Message = "Successfull",
                Data = department.Data.Staffs.Select(p => new StaffDto
                {
                    FirstName = p.User.FirstName,
                    LastName = p.User.LastName,
                    Id = p.Id,
                    DepartmentName = p.Department.DepartmentName,
                    StaffNumber = p.StaffNumber,
                    Level = p.Level,
                    Position = p.Position,
                    Email = p.User.Email,
                    Gender = (GenderEnum)p.User.Gender,
                    ImageUrl = p.User.ProfilePhotoUrl

                }).ToList()
            };
            return View(staffs.Data);
        }
    }
}
