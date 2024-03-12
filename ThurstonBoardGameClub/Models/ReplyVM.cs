using System;
using System.ComponentModel.DataAnnotations;

namespace ThurstonBoardGameClub.Models
{
    public class ReplyVM
    {
        public int MessageId { get; set; }    // This identifies the message being replied to
        public string From { get; set; }

        /*[Required(ErrorMessage = "You must provide a reply body.")]*/
        public string ReplyText { get; set; }
    }
}
