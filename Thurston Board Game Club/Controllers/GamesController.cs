using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Thurston_Board_Game_Club.Models;

namespace Thurston_Board_Game_Club.Controllers
{
    public class GamesController : Controller
    {
        private readonly ILogger<GamesController> _logger;

        public GamesController(ILogger<GamesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DonateGames()
        {
            return View();
        }

        public IActionResult OwnedGames()
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
