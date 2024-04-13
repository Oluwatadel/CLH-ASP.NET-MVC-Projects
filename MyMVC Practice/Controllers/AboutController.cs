using Microsoft.AspNetCore.Mvc;
using MyMVC_Practice.Context;
using MyMVC_Practice.Models.ViewModels;

namespace MyMVC_Practice.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult About()
        {
            return View();
        }

        public IActionResult AboutStaff()
        {
            var staffs = DataBase.staffs;
            var staff = staffs.Select(p => new StaffViewModel
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                PhoneNumber = p.PhoneNumber,
                Department = p.Department,
                StaffNumber = p.StaffNumber,
                Level = p.Level,
                Position = p.Position,
                Gender = p.Gender,
                Email = p.Email,
            }).ToList();
            return View(staff);
        }
    }
}
