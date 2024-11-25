using LR13.Models;
using LR13.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LR13.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserService _userService;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Дія для обробки логіну або реєстрації
        public IActionResult LoginRegister()
        {
            // Повертаємо сторінку Login/Register для GET-запиту
            return View();
        }

        [HttpPost]
        public IActionResult LoginRegister(string username, string email, string password, string action)
        {
            _logger.LogInformation("User {Name} attempted action '{Action}' at {Time}", username, action, DateTime.Now);

            if (password.Length < 6)
            {
                _logger.LogWarning("Password for user {Name} is too short", username);
                ViewData["ErrorMessage"] = "Password must be at least 6 characters long.";
                _logger.LogError("This is a critical error: Password must be at least 6 characters long.");
                return View();
            }

            if (action == "Login")
            {
                if (username != null && email != null && password != null)
                {
                    _logger.LogInformation("User {Name} logged in successfully at {Time}", username, DateTime.Now);
                    return RedirectToAction("Index");
                }
                else
                {
                    _logger.LogWarning("Invalid login attempt for user {Name}", username);
                    ViewData["ErrorMessage"] = "Invalid username or password.";
                    return View();
                }
            }
            else if (action == "Register")
            {
                _logger.LogInformation("User {Name} registered and logged in at {Time}", username, DateTime.Now);

                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Index(string a, string b)
        {
            _logger.LogInformation("Today is a good day!");
            //
            _logger.LogDebug("Debug level log");
            _logger.LogInformation("Information level log");
            _logger.LogWarning("Warning level log");
            _logger.LogError("Error level log");
            _logger.LogCritical("Critical level log");
            //
            if (double.TryParse(a, out double numA) && double.TryParse(b, out double numB))
            {
                double result = numA * numB;
                ViewData["Result"] = result;
            }
            else
            {
                ViewData["Result"] = "Invalid input";
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
