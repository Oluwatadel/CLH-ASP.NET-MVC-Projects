using DMSMVC.Models.DTOs;
using DMSMVC.Models.RequestModel;
using DMSMVC.Service.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace Web.Controllers
{

    public class UserController : Controller
    {
        public readonly IUserService _userService;
        public readonly IStaffService _staffService;
        public readonly IDepartmentService _departmentService;
        public readonly IIdentityService _identityService;

        public UserController(IUserService userService, IStaffService staffService, IDepartmentService departmentService, IIdentityService identityService)
        {
            _userService = userService;
            _staffService = staffService;
            _departmentService = departmentService;
            _identityService = identityService;

        }
        
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var isValidCredntial = await _identityService.IsCredentialsValid(loginRequest);
            if(!isValidCredntial)
            {
                TempData["Message"] = "Invalid Credential!!";
				return View(loginRequest);
            }
            var user = await _userService.GetUserAsyn(loginRequest.Email);

            //Read on this
            var claims = new List<Claim>
            {
                new Claim("Name", $"{user.LastName} {user.FirstName}"),
                new Claim("Email", user.Email),
                new Claim("Id", user.Id.ToString()),
                new Claim("DepartmentId", user.Staff.DepartmentId.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(claimsIdentity);
            var property = new AuthenticationProperties();
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, property);
            TempData["Name"] = claims[0].ToString();
			return RedirectToAction("Dashboard");
        }

        public async Task<IActionResult> Register()
        {
            var departments = await _departmentService.GetAllDepartment();
            TempData["Departments"] = departments.Data;

			return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(LoginRequestModel request)
        {
            var user = await _userService.(request);
            if(user.Data == null)
			{
                TempData["Error"] = user.Message;
				return View(request);
            }
            return View("Login");
            }

        public IActionResult Dashboard(UserDTO userDTO)
        {
            return View(userDTO);
        }
    }
}
