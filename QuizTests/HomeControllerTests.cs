using ThurstonBoardGameClub.Controllers;
using ThurstonBoardGameClub.Data;
using ThurstonBoardGameClub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using Xunit;

namespace ThurstonBoardGameClub.Controllers
{
    public class MessageControllerTests
    {
        IMessageRepository repo = new FakeMessageRepository();
        HomeController controller;

        // IMPORTANT NOTE: Overloaded Method in HomeController.cs must be uncommented for testing, must be re-commented for site loading as MVC does not allow overloading Controller Actions when run.

        // Must be uncommented for testing, as well as above information.
/*        public MessageControllerTests()
        {
            controller = new HomeController(repo);
        }*/

        [Fact]
        public void Message_PostTest_Success()
        {
            // arrange
            // Done in the constructor

            // act
            var result = controller.Message(new Message());

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
            var result = controller.Message(null);

            // assert
            // Check to see if I got a RedirectToActionResult
            Assert.True(result.GetType() == typeof(ViewResult));
        }
    }
}