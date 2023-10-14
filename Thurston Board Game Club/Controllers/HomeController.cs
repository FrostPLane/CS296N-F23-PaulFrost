using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Thurston_Board_Game_Club.Models;

namespace Thurston_Board_Game_Club.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult History()
        {
            return View();
        }

        public IActionResult Message()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Message(Message model)
        {
            if (ModelState.IsValid)
            {
                model.Date = DateTime.Now;
                return RedirectToAction("UserMessage", "Home", model);
            }
            else
            {
                var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();
            }

            return View();
        }

        public IActionResult UserMessage(Message model)
        {
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}