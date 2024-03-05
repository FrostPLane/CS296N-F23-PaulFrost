using Microsoft.AspNetCore.Authorization;
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
        IReplyRepository rerepo;

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
        public IActionResult Reply(int messageId)
        {
            var replyVM = new ReplyVM { MessageId = messageId };
            return View(replyVM);
        }

        [HttpPost]
        public async Task<RedirectToActionResult> Reply(ReplyVM replyVM)
        {
            // Reply is the domain model
            var reply = new Reply { ReplyText = replyVM.ReplyText };
            reply.From = userm.GetUserAsync(User).Result;
            reply.ReplyDate = DateTime.Now;

            // Retrieve the message that this reply is for
            var message = (from r in repo.Messages.Include(r => r.Replies)
                           where r.MessageId == replyVM.MessageId
                           select r).First<Message>();

            reply.MessageId = message.MessageId;
            // Store the message with the reply in the database
            message.Replies.Add(reply);
            await rerepo.StoreReplyAsync(reply);

            return RedirectToAction("FromQuery", new { messageName = message.From });
        }

        /*        [HttpPost]
                public async Task<RedirectToActionResult> Reply(ReplyVM replyVM)
                {
                    // Reply is the domain model
                    var reply = new Reply { ReplyText = replyVM.ReplyText };
                    reply.From = userm.GetUserAsync(User).Result;
                    reply.ReplyDate = DateTime.Now;

                    // Retrieve the message that this reply is for
                    var message = (from r in repo.Messages.Include(r => r.Replies)
                                  where r.MessageId == replyVM.MessageId
                                  select r).First<Message>();

                    // Store the message with the reply in the database
                    message.Replies.Add(reply);
                    await repo.StoreMessageAsync(message);

                    return RedirectToAction("FromQuery", new { messageName = message.From });
                }*/
    }
}