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

        public HomeController(IMessageRepository r, IReplyRepository ir, ILogger<HomeController> logger, UserManager<AppUser> um)
        {
            _logger = logger;
            repo = r;
            userm = um;
            rerepo = ir;
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
                messages = await repo.Messages.Include(m => m.Replies).ThenInclude(r => r.From).ToListAsync();
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
        [HttpGet]
        public IActionResult Message()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Message(Message message)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userm.FindByNameAsync(User.Identity.Name);
                message.From = user.UserName;
                await repo.StoreMessageAsync(message);

                return RedirectToAction("Messageboard", "Home");
            }

            return View(message);
        }

        /*public IActionResult DeleteMessage(int messageId)
        {
            // TODO: Do something like redirect if the delete fails
            repo.DeleteMessageAsync(messageId);
            return RedirectToAction("MessageBoard", "Home");
        }*/

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

            reply.MessageId = replyVM.MessageId;

            await rerepo.StoreReplyAsync(reply);

            return RedirectToAction("MessageBoard", "Home");
        }
    }
}