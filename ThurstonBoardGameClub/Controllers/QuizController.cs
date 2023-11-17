using Microsoft.AspNetCore.Mvc;
using ThurstonBoardGameClub.Models;

namespace ThurstonBoardGameClub.Controllers
{
    public class QuizController : Controller
    {
        public Dictionary<int, String> Questions { get; set; }
        public Dictionary<int, String> Answers { get; set; }

        public QuizController()
        {
            Questions = new Dictionary<int, String>();
            Answers = new Dictionary<int, String>();
            Questions[1] = "Does The Monopoly Mascot' Wear Glasses?";
            Answers[1] = "No";
            Questions[2] = "In Sorry! Do You Have To Apologize?";
            Answers[2] = "No";
            Questions[3] = "In Blackjack, Is A Joke A Wildcard?";
            Answers[3] = "No";
            Questions[4] = "In Catan, Can You Say 'I Offer You 10 Sheep For 20 Wood'?";
            Answers[4] = "Yes";
        }

        public IActionResult Index()
        {
            var model = LoadQuestions(new QuizQuestions());
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(string answer1, string answer2, string answer3, string answer4)
        {
            var model = LoadQuestions(new QuizQuestions());
            model.InputAnswers[1] = answer1;
            model.InputAnswers[2] = answer2;
            model.InputAnswers[3] = answer3;
            model.InputAnswers[4] = answer4;

            var checkedModel = checkQuizAnswers(model);
            return View(checkedModel);
        }

        public QuizQuestions LoadQuestions(QuizQuestions model)
        {
            model.Questions = Questions;
            model.Answers = Answers;
            model.InputAnswers = new Dictionary<int, string>();
            model.Results = new Dictionary<int, bool>();
            foreach (var question in Questions)
            {
                int key = question.Key;
                model.InputAnswers[key] = "";
            }

            return model;
        }

        public QuizQuestions checkQuizAnswers(QuizQuestions model)
        {
            foreach (var question in Questions)
            {
                int key = question.Key;
                model.Results[key] = model.Answers[key] == model.InputAnswers[key];
            }
            return model;
        }
    }
}
