namespace Welsby.Surveys.DataLayer.Models
{
    public class Question
    {
        public int QuestionId { get; private set; }
        public string Text { get; private set; }
        
        // ---------------------------------
        // Relationships
        
        public QuestionType QuestionType { get; private set; }
        
        public QuestionGroup QuestionGroup { get; private set; }


        private Question() { }

        public Question(string text, QuestionType type, QuestionGroup questionGroup = null)
        {
            Text = text;
            QuestionType = type;
            QuestionGroup = questionGroup;
        }
    }
}
