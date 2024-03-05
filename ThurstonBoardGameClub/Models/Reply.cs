using System;

namespace ThurstonBoardGameClub.Models
{
    public class Reply
    {
        public int ReplyId { get; set; }
        public String ReplyText { get; set; }
        public DateTime ReplyDate { get; set; }
        public AppUser From { get; set; }
        public int MessageId { get; set; }  // FK to cause cascade delete
    }
}
