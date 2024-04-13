using DMSMVC.Models.DTOs;
using DMSMVC.Models.Entities;
using DMSMVC.Service.Implementation;
using DMSMVC.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DMSMVC.Controllers
{
    public class StaffController : Controller
    {
        public readonly IUserService _userService;
        public readonly IStaffService _staffService;
        private readonly IDepartmentService _departmentService;

        public StaffController(IUserService userService, IStaffService staffService, IDepartmentService departmentService)
        {
            _userService = userService;
            _staffService = staffService;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> SearchForAStaff()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchForAStaff(string emailOrStaffNumber)
        {
            var staff = await _staffService.GetStaffByStaffNumber(emailOrStaffNumber);
            return RedirectToAction("StaffDetail", staff.Data);
        }



        public async Task<IActionResult> StaffDetail(StaffDto staff)
        {
            return View(staff);
        }


        public async Task<IActionResult> CreateStaff()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateStaff(UserRequestModel userRequestModel)
        {
            await _staffService.CreateAsync(userRequestModel);
            return RedirectToAction("Login", "User");
        }
        
        
        public async Task<IActionResult> UpdateStaff(string id)
        {
            var staff = await _staffService.GetStaffById(id);
            if (staff == null)
            {
                return View();
            }
            var departments = await _departmentService.GetAllDepartment();
            TempData["Departments"] = departments.Data;
            return View(staff);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStaff(string id, UserRequestModel requestModel)
        {
            await _staffService.UpdateStaffAsync(id, requestModel);
            return RedirectToAction("DisplayAllStaffsOfDepartment", "Department");
        }


        
    }
}
