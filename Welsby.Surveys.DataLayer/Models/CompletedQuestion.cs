namespace Welsby.Surveys.DataLayer.Models
{
    public class CompletedQuestion
    {
        public int CompletedQuestionId { get; private set; }

        public Question Question { get; private set; }

        public string Answer { get; private set; }

        // -----------------------------------------
        // Relationships

        public CompletedSurvey CompletedSurvey { get; private set; }


        private CompletedQuestion() { }

        public CompletedQuestion(Question question, string answer, CompletedSurvey completedSurvey = null)
        {
            Question = question;
            Answer = answer;
            CompletedSurvey = completedSurvey;
        }


    }
}
