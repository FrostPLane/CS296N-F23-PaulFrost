using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThurstonBoardGameClub.Models
{
    public class Reply
    {
        public int ReplyId { get; set; }
        public string ReplyText { get; set; }
        public DateTime ReplyDate { get; set; }
        public AppUser From { get; set; }

        [ForeignKey("Message")]
        public int MessageId { get; set; }  // FK to cause cascade delete

        public virtual Message Message { get; set; }
    }
}
