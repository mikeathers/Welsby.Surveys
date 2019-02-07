using System.Collections.Generic;
using System.Linq;
using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.GenericInterfaces.GenericInterfaces;

namespace Welsby.Surveys.DataLayer.Models
{
    public class QuestionGroup
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private HashSet<Question> _questions;

        public int QuestionGroupId { get; private set; }

        public string Name { get; private set; }

        public bool SoftDelete { get; private set; }

        // ---------------------------------
        // Relationships

        public IEnumerable<Question> Questions => _questions?.ToList();

        
        public Survey Survey { get; private set; }
       

        private QuestionGroup() { }

        public QuestionGroup(string name, IEnumerable<Question> questions, Survey survey = null)
        {
            Name = name;
            if (survey != null) Survey = survey;

            _questions = new HashSet<Question>();
        
            foreach (var question in questions)
            {
                AddQuestion(question.Text, question.QuestionType);
            }
        }

        public StatusGenericHandler AddQuestion(string text, QuestionType type, SurveyDbContext context = null)
        {
            var status = new StatusGenericHandler();
            var question = new Question(text, type, this);

            if (_questions != null)
            {
                _questions.Add(question);
            }
            else if (context == null)
            {
                status.AddError("You must provide a context if you want to remove this Survey.", nameof(context));
                return status;
            }
            else if (context.Entry(this).IsKeySet)
            {
                context.Add(new Question(text, type, this));
            }
            else
            {
                status.AddError("Could not add a new survey.", nameof(context));
                return status;
            }

            return status;
        }


    }
}
