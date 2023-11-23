using ThurstonBoardGameClub.Models;

namespace ThurstonBoardGameClub.Data
{
    public interface IMessageRepository
    {
        public Message GetMessageById(int id);
        public int StoreMessage(Message model);
    }
}
