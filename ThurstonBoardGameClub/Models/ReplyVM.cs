using System;

namespace ThurstonBoardGameClub.Models
{
    public class ReplyVM
    {
        public int MessageId { get; set; }    // This identifies the message being replied to
        public string From { get; set; }
        public String ReplyText { get; set; }
    }
}
