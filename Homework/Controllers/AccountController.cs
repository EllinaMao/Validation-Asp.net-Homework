using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Homework.Models;
using Homework.Services;
using Homework.ViewModels;

namespace Homework.Controllers
{
    public class AccountController : Controller
    {
        private readonly RegisterService _registerService;

        public AccountController(RegisterService registerService)
        {
            _registerService = registerService;
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> IsUsernameAvailable(string username)
        {
            var isAvailable = await _registerService.IsUsernameAvailableAsync(username);

            if (isAvailable)
            {
                return Json(true);
            }

            return Json($"Username '{username}' is already taken.");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
           
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = model.Username,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Age = model.Age,
                    Website = model.Website,
                    CreditCardNumber = model.CreditCardNumber,

                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                    RegistrationDate = DateTime.UtcNow
                };

                await _registerService.AddUserAsync(user);

                return Content("Регистрация прошла успешно и пользователь сохранен в базу данных!");
            }

            return View(model);
        }
    }
}