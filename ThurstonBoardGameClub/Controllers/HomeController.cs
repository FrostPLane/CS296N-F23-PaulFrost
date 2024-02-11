﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ThurstonBoardGameClub.Data;
using ThurstonBoardGameClub.Models;

namespace ThurstonBoardGameClub.Controllers
{
    public class HomeController : Controller
    {
        ILogger<HomeController> _logger;
        IMessageRepository repo;
        UserManager<AppUser> userm;

        public HomeController(IMessageRepository r, ILogger<HomeController> logger, UserManager<AppUser> um)
        {
            _logger = logger;
            repo = r;
            userm = um;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> MessageBoard(String messageFrom, String messageDate)
        {
            List<Message> messages;

            // filter by message from
            if (messageFrom != null)
            {
                messages = await FromQuery(messageFrom).ToListAsync<Message>();
            }
            else if (messageDate != null)
            {
                messages = await DateQuery(messageDate).ToListAsync<Message>();
            }
            // All query parameters are null
            else
            {
                messages = await repo.Messages.ToListAsync<Message>();
            }

            return View(messages);
        }

        private IQueryable<Message> FromQuery(string messageName)
        {
            return repo.Messages
                .Where(r => r.From == messageName)
                .Select(r => r);
        }

        private IQueryable<Message> DateQuery(string messageDate)
        {
            return repo.Messages
                   .Where(r => r.Date == DateTime.Parse(messageDate).Date)
                   .Select(r => r);
        }


        public IActionResult History()
        {
            return View();
        }

        [Authorize]
        public IActionResult Message()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Message(Message model)
        {
            AppUser user = await userm.FindByNameAsync(User.Identity.Name);
            model.From = user.UserName;
            await repo.StoreMessageAsync(model);

            return RedirectToAction("Message", new { model.MessageId });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}