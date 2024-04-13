using Microsoft.AspNetCore.Mvc;
using MySchool.Core.Application.Dtos;
using MySchool.Core.Application.Interfaces.Services;

namespace MySchool.Controllers
{
    public class GuardianController : Controller
    {
        private readonly IGuardianService _guardianService;
        public GuardianController(IGuardianService guardianService)
        {
            _guardianService = guardianService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(GuardianRequest model)
        {
            var response = await _guardianService.CreateAsync(model);
            if(response == null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Details(string id)
        {
            var response = await _guardianService.GetAsync(id);
            if(response == null)
            {
                return RedirectToAction("Guardians");
            }
            return View(response.Data);
        }

        public async Task<IActionResult> Guardians()
        {
            var response = await _guardianService.GetAllAsync();
            return View(response.Data);
        }
    }
}
