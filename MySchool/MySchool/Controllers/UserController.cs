using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MySchool.Core.Application.Dtos;
using MySchool.Core.Application.Interfaces.Services;
using System.Security.Claims;

namespace MySchool.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) 
        { 
            _userService = userService;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var response = await _userService.LoginAsync(request);
            if(!response.Status)
            {
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, response.Data.Id),
                new Claim(ClaimTypes.Name, response.Data.FullName),
                new Claim(ClaimTypes.Email, response.Data.Email),
                //new Claim("Image", response.Data.ImageUrl),
                new Claim("Age", response.Data.Age.ToString()),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties();

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal, properties);

            return RedirectToAction("Index", "Home");
            //if (response.Data.Roles.Select(a => a.Name).Contains("SuperAdmin"))
            //{
            //    return RedirectToAction("Index");
            //}
            //return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
