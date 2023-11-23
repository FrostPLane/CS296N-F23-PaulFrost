using ThurstonBoardGameClub.Data;
using ThurstonBoardGameClub.Models;
using System.Collections.Generic;

namespace ThurstonBoardGameClub
{
    public class FakeMessageRepository : IMessageRepository
    {
        private List<Message> messages = new List<Message>();

        public Message GetMessageById(int id)
        {
            Message message = messages.Find(m => m.MessageId == id);

            return message;
        }

        public int StoreMessage(Message model)
        {
            int status = 0;
            if (model != null)
            {
                model.MessageId = messages.Count + 1;
                messages.Add(model);
                status = 1;
            }
            return status;
        }

    }
}