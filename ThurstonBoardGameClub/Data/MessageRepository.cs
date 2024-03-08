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

        public async Task<Message> GetMessageByIdAsync(int id)
        {
            var message = context.Messages
              .Include(message => message.From)
              .Include(message => message.Text)
              .Where(message => message.MessageId == id)
              .SingleOrDefault();
            // return message
            return await context.Messages.FindAsync(id);
        }

        public async Task<int> StoreMessageAsync(Message model)
        {
            model.Date = DateTime.Now;
            context.Messages.Add(model);
            return await context.SaveChangesAsync();
            // returns a positive value if succussful
        }

        public int UpdateMessage(Message message)
        {
            context.Update(message);
            // Returns the number of updated saved
            return context.SaveChanges();
        }

        public async Task<int> DeleteMessageAsync(int messageId)
        {
            Message message = GetMessageByIdAsync(messageId).Result;
            context.Messages.Remove(message);
            return await context.SaveChangesAsync();
        }
    }
}
