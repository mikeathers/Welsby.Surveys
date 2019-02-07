using System;
using System.Collections.Generic;
using System.Linq;
using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.GenericInterfaces.GenericInterfaces;


namespace Welsby.Surveys.DataLayer.Models
{
    public class Survey
    {

        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private HashSet<QuestionGroup> _questionGroups;

        public int SurveyId { get; private set; }

        public string Name { get; private set; }

        public bool SoftDelete { get; private set; }

        // ---------------------------------
        // Relationships

        public IEnumerable<QuestionGroup> QuestionGroups => _questionGroups?.ToList();
       

        private Survey() { }

        public Survey(string name, IEnumerable<QuestionGroup> questionGroups)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name), "A name is needed when creating a new survey.");
            _questionGroups = new HashSet<QuestionGroup>();

            foreach (var questionGroup in questionGroups)
            {
                AddQuestionGroup(questionGroup.Name, questionGroup.Questions);
            }
        }


        public IStatusGeneric AddQuestionGroups(IEnumerable<QuestionGroup> questionGroups, SurveyDbContext context = null)
        {
            var status = new StatusGenericHandler();
            if (questionGroups == null)
            {
                status.AddError("No QuestionGroups have been provided.", nameof(questionGroups));
                return status;
            }

            foreach (var questionGroup in questionGroups)
            {
                status = AddQuestionGroup(questionGroup.Name, questionGroup.Questions, context);
            }

            return status;
        }

        private StatusGenericHandler AddQuestionGroup(string name, IEnumerable<Question> questions, SurveyDbContext context = null)
        {
            var status = new StatusGenericHandler();

            if (string.IsNullOrWhiteSpace(name))
            {
                status.AddError("A name is needed when creating a new question group.", nameof(name));
                return status;
            }

            if (_questionGroups != null)
            {
                var questionGroup = new QuestionGroup(name, questions, this);
                _questionGroups.Add(questionGroup);
            }
            else if (context == null)
            {
                status.AddError("You must provide a context if the QuestionGroups collection isn't valid.", nameof(context));
                return status;
            }
            else if (context.Entry(this).IsKeySet)
            {
                context.Add(new QuestionGroup(name, questions, this));
            }
            else
            {
                status.AddError("Could not add a new QuestionGroup.");
                return status;
            }

            return status;
        }


        public StatusGenericHandler AddQuestions(IEnumerable<Question> questions, QuestionGroup questionGroup, SurveyDbContext context = null)
        {
            var status = new StatusGenericHandler();
            if (questions == null)
            {
                status.AddError("No Questions have been provided", nameof(questions));
                return status;
            }

            foreach (var question in questions)
            {
                status = AddQuestion(question.Text, question.QuestionType, questionGroup, context);
            }
            return status;
        }

        private StatusGenericHandler AddQuestion(string text, QuestionType questionType, QuestionGroup questionGroup, SurveyDbContext context = null)
        {
            var status = new StatusGenericHandler();
            

            if (string.IsNullOrWhiteSpace(text))
            {
                status.AddError("Question text is needed when creating a new question.", nameof(text));
                return status;
            }

            if (questionType == null)
            {
                status.AddError("A QuestionType has not been specified.", nameof(questionType));
                return status;
            }
            if (questionGroup == null)
            {
                status.AddError("A QuestionGroup which to add this question to has not been specified.", nameof(questionGroup));
                return status;
            }
            if (context != null)
            {
                status = questionGroup.AddQuestion(text, questionType, context);
            }
            else
            {
                status.AddError("Could not add a new Question.");
                return status;
            }

            return status;

        }


        public StatusGenericHandler RemoveSelf(SurveyDbContext context = null)
        {
            var status = new StatusGenericHandler();

            if (context == null)
            {
                status.AddError("You must provide a context if you want to remove this Survey.", nameof(context));
                return status;
            }

            SoftDelete = true;
            return status;
        }

        public StatusGenericHandler RenameSurvey(string newName, SurveyDbContext context = null)
        {
            var status = new StatusGenericHandler();

            if (string.IsNullOrWhiteSpace(newName))
            {
                status.AddError("A new name has not been provided");
                return status;
            }

            if (context == null)
            {
                status.AddError("You must provide a context if you want to remove this Survey.", nameof(context));
                return status;
            }

            var nameAlreadyTaken = context.Surveys.Any(m => m.Name == newName);

            if (nameAlreadyTaken)
            {
                status.AddError("A survey with the name you have provided already exists.", nameof(context));
                return status;
            }

            Name = newName;
            return status;
        }

    }
}
