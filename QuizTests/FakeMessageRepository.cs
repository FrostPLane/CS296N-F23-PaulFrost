using ThurstonBoardGameClub.Data;
using ThurstonBoardGameClub.Models;
using System.Collections.Generic;
using System.Linq;
using TestDoubles;

namespace ThurstonBoardGameClub
{
    public class FakeMessageRepository : IMessageRepository
    {
        private List<Message> messages = new List<Message>();

        IQueryable<Message> IMessageRepository.Messages => throw new System.NotImplementedException();
        public Message GetMessageById(int id)
        {
            Message message = messages.Find(m => m.MessageId == id);

            return message;
        }
        public IQueryable<Message> Messages
        {
            get
            {
                return new InMemoryAsyncQueryable<Message>(Messages);
            }
        }

        public int StoreMessageAsync(Message model)
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

        Message IMessageRepository.GetMessageById(int id)
        {
            throw new System.NotImplementedException();
        }

        Task<int> IMessageRepository.StoreMessageAsync(Message model)
        {
            throw new System.NotImplementedException();
        }
    }
}