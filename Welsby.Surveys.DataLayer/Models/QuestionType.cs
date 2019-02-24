namespace Welsby.Surveys.DataLayer.Models
{
    public class QuestionType
    {
        public int QuestionTypeId { get; private set; }
        public string Name { get; private set; }


        private QuestionType() { }

        public QuestionType(string name)
        {
            Name = name;
        }

    }
}
