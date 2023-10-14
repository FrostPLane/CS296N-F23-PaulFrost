﻿using Microsoft.AspNetCore.Mvc;
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

        public IActionResult UserMessage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(Message model)
        {
            model.Date = DateTime.Now;
            return RedirectToAction("UserMessage", "Home", model);
/*            return View("Home/UserMessage.cshtml", model);*/
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}