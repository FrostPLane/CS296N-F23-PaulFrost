﻿using ThurstonBoardGameClub.Models;
using Microsoft.AspNetCore.Identity;

namespace ThurstonBoardGameClub.Data
{
    public class SeedData
    {
        public static void Seed(AppDbContext context, IServiceProvider provider)
        {
            if (!context.Messages.Any())  // this is to prevent duplicate data from being added
            {
                var userManager = provider.GetRequiredService<UserManager<AppUser>>();
                Message message = new Message
                {
                    MessageId = 1,
                    From = "First Name",
                    To = "First To",
                    Subject = "First Subject",
                    Text = "First Text",
                    Priority = 1,
                    Date = DateTime.Parse("11/1/2023")

                };
                context.Messages.Add(message);  // queues up the message to be added to the DB

                message = new Message
                {
                    MessageId = 2,
                    From = "Second Name",
                    To = "Second To",
                    Subject = "Second Subject",
                    Text = "Second Text",
                    Priority = 2,
                    Date = DateTime.Parse("11/2/2023")
                };
                context.Messages.Add(message);

                context.SaveChanges(); // stores all the reviews in the DB
            }
        }
    }
}
