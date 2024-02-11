﻿using ThurstonBoardGameClub.Controllers;
using ThurstonBoardGameClub.Data;
using ThurstonBoardGameClub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using Xunit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace ThurstonBoardGameClub.Controllers
{
    public class MessageControllerTests
    {
        IMessageRepository repo = new FakeMessageRepository();
        private readonly ILogger<HomeController> logger;
        private readonly UserManager<AppUser> userm;

        HomeController controller;

        public MessageControllerTests()
        {
            controller = new HomeController(repo, logger, userm);
        }

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