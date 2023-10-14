namespace Thurston_Board_Game_Club.Models
{
    public class Message
    {
        public AppUser To { get; set; }
        public AppUser From { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
