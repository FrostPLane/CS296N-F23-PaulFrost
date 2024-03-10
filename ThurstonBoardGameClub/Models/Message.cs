using System.ComponentModel.DataAnnotations;

namespace ThurstonBoardGameClub.Models
{
    public class Message
    {
        public int MessageId { get; set; }

        [StringLength(40, ErrorMessage = "The To string length cannot exceed 40 characters")]
        public string To { get; set; }
        public string From { get; set; }

        [MinLength(4, ErrorMessage = "You must provide a message subject minimum 4 characters.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "You must provide a message body.")]
        public string Text { get; set; }

        public int Priority { get; set; }
        public DateTime Date { get; set; }

        // if there are replies this property will reference them
        public ICollection<Reply> Replies { get; set; }
    }
}
