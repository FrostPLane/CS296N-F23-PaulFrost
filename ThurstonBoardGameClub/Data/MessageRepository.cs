using ThurstonBoardGameClub.Models;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace ThurstonBoardGameClub.Data
{
    public class MessageRepository : IMessageRepository
    {
        private AppDbContext context;

        public MessageRepository(AppDbContext appDbContext)
        {
            context = appDbContext;
        }

        public IQueryable<Message> Messages
        {
            get
            {
                return context.Messages.Select(m => m);
            }
        }

        public Message GetMessageById(int id)
        {
            var message = context.Messages
              .Include(message => message.From)
              .Include(message => message.Text)
              .Where(message => message.MessageId == id)
              .SingleOrDefault();
            return message;
        }

        public async Task<int> StoreMessageAsync(Message model)
        {
            model.Date = DateTime.Now;
            context.Messages.Add(model);
            return await context.SaveChangesAsync();
            // returns a positive value if succussful
        }
    }
}
