using ThurstonBoardGameClub.Models;

namespace ThurstonBoardGameClub.Data
{
    public interface IMessageRepository
    {
        IQueryable<Message> Messages { get; }
        public Task<Message> GetMessageByIdAsync(int id);
        public Task<int> StoreMessageAsync(Message model);
        public int UpdateMessage(Message message);
        public Task<int> DeleteMessageAsync(int messageId);
    }
}
