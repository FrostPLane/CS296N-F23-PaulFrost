namespace ThurstonBoardGameClub.Models
{
    public enum QuestionType
    {
        ShortAnswer,
        Numeric,
        MultipleChoice,
        TrueFalse
    }

    public class QuestionVM
    {
        public QuestionType Type { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }      // the right answer.
        public string UserAnswer { get; set; }
        public bool? IsRight { get; set; } = null;  
        // true if the user answer is right.
        // IsRight is nullable and initialized to null to show that the answer hasn't been checked.
    }
}