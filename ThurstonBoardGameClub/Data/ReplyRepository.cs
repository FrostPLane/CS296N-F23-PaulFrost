using ThurstonBoardGameClub.Models;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace ThurstonBoardGameClub.Data
{
    public class ReplyRepository : IReplyRepository
    {
        private AppDbContext context;

        public ReplyRepository(AppDbContext appDbContext)
        {
            context = appDbContext;
        }

        public IQueryable<Reply> Replies
        {
            get
            {
                return context.Replies.Select(m => m);
            }
        }

        public async Task<Reply> GetReplyByIdAsync(int id)
        {
            var reply = context.Replies
              .Include(reply => reply.From)
              .Include(reply => reply.ReplyText)
              .Where(reply => reply.ReplyId == id)
              .SingleOrDefault();
            // return reply
            return await context.Replies.FindAsync(id);
        }

        public async Task<int> StoreReplyAsync(Reply model)
        {
            model.ReplyDate = DateTime.Now;
            context.Replies.Add(model);
            return await context.SaveChangesAsync();
            // returns a positive value if succussful
        }
    }
}
