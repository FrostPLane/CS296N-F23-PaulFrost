namespace ThurstonBoardGameClub.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public int Priority { get; set; }
        public DateTime Date { get; set; }

        // if there are replies this property will reference them
        public List<Reply> Replies { get; set; } = new List<Reply>();
        public int? idOriginalMessage { get; set; } = null;
    }
}
