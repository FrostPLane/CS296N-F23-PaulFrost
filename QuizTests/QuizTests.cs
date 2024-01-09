using ThurstonBoardGameClub.Controllers;
using ThurstonBoardGameClub.Models;

namespace BoardGameTests
{
    public class QuizTests
    {
        [Fact]
        public void TestLoadQuestions()
        {
            var controller = new QuizController();
            var model = new QuizQuestions();

            var loadedModel = controller.LoadQuestions(model);

            Assert.NotNull(loadedModel.Questions);
            Assert.NotNull(loadedModel.Answers);
            Assert.NotEmpty(loadedModel.Questions);
            Assert.NotEmpty(loadedModel.Answers); Assert.Equal(controller.Questions, loadedModel.Questions);
            Assert.Equal(controller.Answers, loadedModel.Answers);
            Assert.Equal(loadedModel.Questions.Count, loadedModel.Answers.Count);
        }

        [Fact]
        public void TestQuizRightAnswer()
        {
            var model = new QuizQuestions();
            var controller = new QuizController();
            var loadedModel = controller.LoadQuestions(model);
            loadedModel.InputAnswers[1] = "No";
            loadedModel.InputAnswers[2] = "No";
            loadedModel.InputAnswers[3] = "No";
            loadedModel.InputAnswers[4] = "Yes";

            var result = controller.checkQuizAnswers(model);

            Assert.True(result.Results[1]);
            Assert.True(result.Results[2]);
            Assert.True(result.Results[3]);
            Assert.True(result.Results[4]);
        }

        [Fact]
        public void TestQuizNoAnswer()
        {
            var model = new QuizQuestions();
            var controller = new QuizController();
            var loadedModel = controller.LoadQuestions(model);
            loadedModel.InputAnswers[1] = "";
            loadedModel.InputAnswers[2] = "";
            loadedModel.InputAnswers[3] = "";
            loadedModel.InputAnswers[4] = "";

            var result = controller.checkQuizAnswers(model);

            Assert.False(result.Results[1]);
            Assert.False(result.Results[2]);
            Assert.False(result.Results[3]);
            Assert.False(result.Results[4]);
        }

        [Fact]
        public void TestQuizWrongAnswer()
        {
            var model = new QuizQuestions();
            var controller = new QuizController();
            var loadedModel = controller.LoadQuestions(model);
            loadedModel.InputAnswers[1] = "Yes";
            loadedModel.InputAnswers[2] = "Yes";
            loadedModel.InputAnswers[3] = "Yes";
            loadedModel.InputAnswers[4] = "No";

            var result = controller.checkQuizAnswers(model);

            Assert.False(result.Results[1]);
            Assert.False(result.Results[2]);
            Assert.False(result.Results[3]);
            Assert.False(result.Results[4]);
        }
    }
}