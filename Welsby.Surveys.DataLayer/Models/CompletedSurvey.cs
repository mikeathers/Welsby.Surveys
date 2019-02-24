using System.Collections.Generic;
using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.GenericInterfaces.GenericInterfaces;

namespace Welsby.Surveys.DataLayer.Models
{
    public class CompletedSurvey
    {
        private readonly HashSet<CompletedQuestion> _completedQuestions;

        public int CompletedSurveyId { get; private set; }
        public string Name { get; private set; }
        public int CaseNumber { get; private set; }
        

        // --------------------------------------
        // Relatiionships

        public IEnumerable<CompletedQuestion> CompletedQuestions => _completedQuestions;



        private CompletedSurvey() { }

        public CompletedSurvey(string name, int caseNo, IEnumerable<CompletedQuestion> questions)
        {
            Name = name;
            CaseNumber = caseNo;
            _completedQuestions = new HashSet<CompletedQuestion>();

            foreach (var question in questions)
            {
                AddQuestion(question);
            }

        }

        public StatusGenericHandler AddQuestion(CompletedQuestion question, SurveyDbContext context = null)
        {

            var status = new StatusGenericHandler();

            if (string.IsNullOrWhiteSpace(question.Answer))
            {
                status.AddError("An answer is needed when submitting a question.", nameof(question.Answer));
                return status;
            }

            if (_completedQuestions != null)
            {
                var completedQuestion = new CompletedQuestion(question.Question, question.Answer, this);
                _completedQuestions.Add(completedQuestion);
            }
            else if (context == null)
            {
                status.AddError("You must provide a context if the CompletedQuestions collection isn't valid.", nameof(context));
                return status;
            }
            else if (context.Entry(this).IsKeySet)
            {
                context.Add(new CompletedQuestion(question.Question, question.Answer, this));
            }
            else
            {
                status.AddError("Could not add a new CompletedQuestion.");
                return status;
            }

            return status;
        }
    }
}
