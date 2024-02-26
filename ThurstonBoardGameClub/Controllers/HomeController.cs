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
        private readonly ILogger<HomeController> _logger;
        public readonly IMessageRepository repo;
        private readonly UserManager<AppUser> userm;

        public HomeController(IMessageRepository r, ILogger<HomeController> logger, UserManager<AppUser> um)
        {
            _logger = logger;
            repo = r;
            userm = um;
        }


        // ONLY UNCOMMENT FOR TESTING, READ BELOW FOR TESTING METHODS
/*        public HomeController(IMessageRepository r)
        {
            repo = r;
        }*/

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult History()
        {
            return View();
        }

        public IActionResult MessageBoard(string userFrom, String date)
        {
            List<Message> messages;/* = _context.Messages.Select(m => m).ToList();*/

            if (userFrom != null)
            {
                messages = repo.Messages.Where(m => m.From == userFrom).ToList();
            }
            // date is not null
            else if (date != null)
            {
                messages = repo.Messages.Where(m => m.Date == DateTime.Parse(date)).ToList();
            }
            // Both query parameters are null
            else
            {
                messages = repo.Messages.ToList();
            }
            return View(messages);
        }

        // COMMENT OUT IF TESTING
        public IActionResult Message()
        {
            return View();
        }

        // COMMENT OUT IF TESTING
        [HttpPost]
        public async Task<IActionResult> Message(Message model)
        {
            AppUser user = await userm.FindByNameAsync(User.Identity.Name);
            model.From = user.UserName;
            await repo.StoreMessageAsync(model);

            return RedirectToAction("Message", new { model.MessageId });
        }

        // USED FOR TESTING ONLY
        /*        [HttpPost]
                public IActionResult Message(Message model)
                {
                    if (repo.StoreMessage(model) > 0)
                    {
                        return RedirectToAction("Index", new { MessageId = model.MessageId });
                    }
                    else
                    {
                        return View();  // TODO: Send an error message to the view
                    }

                }

                public IActionResult Message(int messageId)
                {
                    Message message = repo.GetMessageById(messageId);

                    // If the http request doesn't have a reviewId, then reviewId = 0.
                    var review = context.Messages
                        .Include(review => review.Reviewer) // returns Reivew.AppUser object
                        .Include(review => review.Book) // returns Message.Book object
                        .Where(review => review.ReviewId == reviewId)
                        .SingleOrDefault();  // default is null
                                             // If no review is found, a null is sent to the view.

                    return View(message);
                }*/

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}