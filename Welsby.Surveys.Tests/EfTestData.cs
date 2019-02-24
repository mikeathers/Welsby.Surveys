using System.Collections.Generic;
using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.DataLayer.Models;
using Welsby.Surveys.Dtos;

namespace Welsby.Surveys.Tests
{
    public static class EfTestData
    {
        public static void SeedDataBaseWithSurveys(this SurveyDbContext context)
        {
            context.Surveys.AddRange(CreateSurveys());
            context.CompletedSurveys.AddRange(CreateCompletedSurveys());
            context.QuestionTypes.AddRange(CreateQuestionTypes());
            context.SaveChanges();
        }

        public static List<QuestionType> CreateQuestionTypes()
        {
            return new List<QuestionType>
            {
                new QuestionType("Text"),
                new QuestionType("Scale"),
            };
        }

        public static List<Survey> CreateSurveys()
        {
            var questionGroups = CreateQuestionGroups();

            return new List<Survey>
            {
                new Survey("Test Survey 1", questionGroups),
                new Survey("Test Survey 2", questionGroups),
                new Survey("Test Survey 3", questionGroups),
            };
        }

        public static List<QuestionGroup> CreateQuestionGroups()
        {
            var questions = CreateQuestions();
            return new List<QuestionGroup>
            {
                new QuestionGroup("Question Group 1", questions),
                new QuestionGroup("Question Group 2", questions),
                new QuestionGroup("Question Group 3", questions),
            };
        }

        public static List<Question> CreateQuestions()
        {
            var type = new QuestionType("Text");
            return new List<Question>
            {
                new Question("What activities have you been unable to do?", type),
                new Question("How has your accident effected your sleep?", type),
                new Question("Have you had any mental issues?", type),
            };
        }

        public static List<DataLayer.Models.CompletedSurvey> CreateCompletedSurveys()
        {
            var completedSurveys = new List<DataLayer.Models.CompletedSurvey>();
            var completedQuestions = CreateCompletedQuestions();

            var completedSurvey1 = new DataLayer.Models.CompletedSurvey("Test Survey1", 999, completedQuestions);
            var completedSurvey2 = new DataLayer.Models.CompletedSurvey("Test Survey2", 999, completedQuestions);
            var completedSurvey3 = new DataLayer.Models.CompletedSurvey("Test Survey3", 999, completedQuestions);
            var completedSurvey4 = new DataLayer.Models.CompletedSurvey("Test Survey3", 999, completedQuestions);

            completedSurveys.Add(completedSurvey1);
            completedSurveys.Add(completedSurvey2);
            completedSurveys.Add(completedSurvey3);
            completedSurveys.Add(completedSurvey4);

            return completedSurveys;
        }

        public static List<CompletedQuestion> CreateCompletedQuestions()
        {
            var questions = CreateQuestions();
            var completedQuestion = questions[0];

            return new List<CompletedQuestion>
            {
                new CompletedQuestion(completedQuestion, "Running")
            };
        }

        public static List<CompletedQuestionDto> CreateCompletedQuestionsDto()
        {
            return new List<CompletedQuestionDto>
            {
                new CompletedQuestionDto
                {
                    QuestionId = 1,
                    Answer = "Running"
                }
            };
        }

        public static List<QuestionDto> CreateQuestionsDtos()
        {
            return new List<QuestionDto>
            {
                new QuestionDto
                {
                    Text = "What activities have you been unable to do?",
                    Type = 1
                },
                new QuestionDto
                {
                    Text = "How has your accident effected your sleep?",
                    Type = 1
                },
                new QuestionDto
                {
                    Text = "Have you had any mental issues?",
                    Type = 1
                }
            };
        }

        public static List<QuestionGroupDto> CreateQuestionGroupDtos()
        {
            var questions = CreateQuestionsDtos();
            return new List<QuestionGroupDto>
            {
                new QuestionGroupDto
                {
                    Name = "Test Question Group A",
                    Questions = questions
                }
            };
        }


        
    }
}
