using ThurstonBoardGameClub.Controllers;
using ThurstonBoardGameClub.Data;
using ThurstonBoardGameClub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using Xunit;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Message = ThurstonBoardGameClub.Models.Message;

namespace ThurstonBoardGameClub.Controllers
{
    public class MessageControllerTests
    {
        IMessageRepository repo = new FakeMessageRepository();
        HomeController controller;
        
        public MessageControllerTests()
        {
            controller = new HomeController(repo, null, null, null);
        }

        [Fact]
        public void Message_PostTest_Success()
        {
            // arrange
            // Done in the constructor

            // act
            var result = controller.Message();

            // assert
            // Check to see if I got a RedirectToActionResult
            Assert.True(result.GetType() == typeof(RedirectToActionResult));
        }

        [Fact]
        public void Message_PostTest_Failure()
        {
            // arrange
            // Done in the constructor

            // act
            var result = controller.Message();

            // assert
            // Check to see if I got a RedirectToActionResult
            Assert.True(result.GetType() == typeof(ViewResult));
        }

        [Fact]
        public void FromQueryTest()
        {
            var message1 = new Message() {  };
            repo.StoreMessageAsync(message1).Wait();
            repo.StoreMessageAsync(message1).Wait();
            var message2 = new Message() {  };
            repo.StoreMessageAsync(message2).Wait();
            repo.StoreMessageAsync(message2).Wait();
            var message3 = new Message() { From = "From 3" };
            repo.StoreMessageAsync(message3).Wait();
            repo.StoreMessageAsync(message3).Wait();

            var controller = new HomeController(repo, null, null, null);

            // Act
            var filteredMessagesView = controller.MessageBoard(message2.From, null).Result as ViewResult;
            List<Message> filteredReviews = filteredMessagesView.Model as List<Message>;

            // Assert
            Assert.Equal(2, filteredReviews.Count);
            Assert.Equal(filteredReviews[0].From, message2.From);
            Assert.Equal(filteredReviews[1].From, message2.From);
        }

        [Fact]
        public void DateQueryTest()
        {
            // Test to see if only reviews with the selected title are returned 

            // Arrange
            // Done in the constructor

            // We don't need need to add all the properties to the models since we aren't testing that.
            var message1 = new Message() { Date = DateTime.Parse("01/01/2020") };
            repo.StoreMessageAsync(message1);
            repo.StoreMessageAsync(message1);
            var message2 = new Message() { Date = DateTime.Parse("06/15/2021") };
            repo.StoreMessageAsync(message2);
            repo.StoreMessageAsync(message2);
            var message3 = new Message() { Date = DateTime.Parse("12/31/2022") };
            repo.StoreMessageAsync(message3);
            repo.StoreMessageAsync(message3);

            var controller = new HomeController(repo, null, null, null);

            // Act
            var filteredReviewsView = controller.MessageBoard(null, message2.Date.ToShortDateString()).Result as ViewResult;
            List<Message> filteredReviews = filteredReviewsView.Model as List<Message>;

            // Assert
            Assert.Equal(2, filteredReviews.Count);
            Assert.Equal(filteredReviews[0].Date, message2.Date);
            Assert.Equal(filteredReviews[1].Date, message2.Date);
        }

    }
}