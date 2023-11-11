using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Thurston_Board_Game_Club.Data;
using Thurston_Board_Game_Club.Models;

namespace Thurston_Board_Game_Club.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        public HomeController(AppDbContext c, ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = c;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult History()
        {
            return View();
        }

        public IActionResult MessageBoard()
        {
            List<Message> messages = _context.Messages.Select(m => m).ToList();
            return View(messages);
        }

        public IActionResult Message()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Message(Message model)
        {
            model.Date = DateTime.Now;

            // Save model to db
            _context.Messages.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Message", new { model.MessageId });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}