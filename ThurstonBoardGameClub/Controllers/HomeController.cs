using Microsoft.AspNetCore.Authorization;
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
        private readonly AppDbContext _context;
        public readonly IMessageRepository repo;

        public HomeController(AppDbContext c, IMessageRepository r, ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = c;
            repo = r;
        }


        // If uncommenting for testing, must all comment out the HttpPost and IActionResult Message() methods, and uncomment the methods below.
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
            var review = context.Reviews
                .Include(review => review.Reviewer) // returns Reivew.AppUser object
                .Include(review => review.Book) // returns Review.Book object
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