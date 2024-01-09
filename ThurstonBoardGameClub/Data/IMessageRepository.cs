using ThurstonBoardGameClub.Models;

namespace ThurstonBoardGameClub.Data
{
    public interface IMessageRepository
    {
        IQueryable<Message> Messages { get; }
        public Message GetMessageById(int id);
        public int StoreMessage(Message model);
    }
}
