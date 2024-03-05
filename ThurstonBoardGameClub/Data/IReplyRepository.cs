using ThurstonBoardGameClub.Models;

namespace ThurstonBoardGameClub.Data
{
    public interface IReplyRepository
    {
        IQueryable<Reply> Replies { get; }
        public Task<Reply> GetReplyByIdAsync(int id);
        public Task<int> StoreReplyAsync(Reply model);
    }
}
