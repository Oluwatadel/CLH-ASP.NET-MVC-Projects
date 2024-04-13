using Microsoft.AspNetCore.Mvc;
using MyMVC_Practice.Models.Service.Implementation;
using MyMVC_Practice.Models.Service.Interface;

namespace MyMVC_Practice.Controllers
{
    public class StaffValidationController : Controller
    {
        IstaffService _staffservice = new StaffService();

        public IActionResult Login()
        {

            return View();
        }

        [HttpPost("GetLogin")]
        public Staff GetLogin(string userName, string password)
        {
            var staff = _staffservice.Login(userName, password);

            return staff;
        }


    }
}
