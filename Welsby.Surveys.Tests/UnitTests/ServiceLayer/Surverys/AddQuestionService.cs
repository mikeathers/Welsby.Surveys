using System.Collections.Generic;
using System.Linq;
using TestSupport.EfHelpers;
using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.Dtos;
using Welsby.Surveys.ServiceLayer.SurveyServices;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace Welsby.Surveys.Tests.UnitTests.ServiceLayer.Surverys
{
    public class AddQuestionService
    {
        [Fact]
        public void TestAddQuestionOk()
        {
            var options = SqliteInMemory.CreateOptions<SurveyDbContext>();
            using (var context = new SurveyDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SeedDataBaseWithSurveys();

                var currentQuestions = context.Questions.Count();
                var questionDtos = new List<QuestionDto>
                {
                    new QuestionDto
                    {
                        Text = "How well have you been sleeping?",
                        Type = 1
                    }
                };

                var surveyDto = new SurveyDto
                {
                    Id = 1,
                    QuestionGroupId = 1,
                    QuestionsDtos = questionDtos
                };

                var service = new Surveys.ServiceLayer.SurveyServices.AddQuestionService(context, new MapQuestionDtoToQuestionsService());
                var result = service.AddQuestion(surveyDto);
                result.ShouldBeNull();
                context.SaveChanges();
                context.Questions.Count().ShouldEqual(currentQuestions + 1);
            }
        }

        [Fact]
        public void TestAddQuestionSurveyNotFound()
        {
            var options = SqliteInMemory.CreateOptions<SurveyDbContext>();
            using (var context = new SurveyDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SeedDataBaseWithSurveys();

                var surveyId = -1;
                var currentQuestions = context.Questions.Count();
                var questionDtos = new List<QuestionDto>
                {
                    new QuestionDto
                    {
                        Text = "How well have you been sleeping?",
                        Type = 1
                    }
                };

                var surveyDto = new SurveyDto
                {
                    Id = surveyId,
                    QuestionGroupId = 1,
                    QuestionsDtos = questionDtos
                };

                var service = new Surveys.ServiceLayer.SurveyServices.AddQuestionService(context, new MapQuestionDtoToQuestionsService());
                var result = service.AddQuestion(surveyDto);
                result.ShouldNotBeNull();
                result.First().ErrorMessage.ShouldEqual($"Could not find Survey with an Id of {surveyId}");
                context.SaveChanges();
                context.Questions.Count().ShouldEqual(currentQuestions);
            }
        }

        [Fact]
        public void TestAddQuestionQuestionGroupNotFound()
        {
            var options = SqliteInMemory.CreateOptions<SurveyDbContext>();
            using (var context = new SurveyDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SeedDataBaseWithSurveys();

                var questionGroupId = -1;
                var currentQuestions = context.Questions.Count();
                var questionDtos = new List<QuestionDto>
                {
                    new QuestionDto
                    {
                        Text = "How well have you been sleeping?",
                        Type = 1
                    }
                };

                var surveyDto = new SurveyDto
                {
                    Id = 1,
                    QuestionGroupId = questionGroupId,
                    QuestionsDtos = questionDtos
                };

                var service = new Surveys.ServiceLayer.SurveyServices.AddQuestionService(context, new MapQuestionDtoToQuestionsService());
                var result = service.AddQuestion(surveyDto);
                result.ShouldNotBeNull();
                result.First().ErrorMessage.ShouldEqual($"Could not find QuestionGroup with an Id of {questionGroupId}");
                context.SaveChanges();
                context.Questions.Count().ShouldEqual(currentQuestions);
            }
        }


        [Fact]
        public void TestAddQuestionQuestionTypeNotSpecified()
        {
            var options = SqliteInMemory.CreateOptions<SurveyDbContext>();
            using (var context = new SurveyDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SeedDataBaseWithSurveys();

                var currentQuestions = context.Questions.Count();
                var questionDtos = new List<QuestionDto>
                {
                    new QuestionDto
                    {
                        Text = "How well have you been sleeping?",
                    }
                };

                var surveyDto = new SurveyDto
                {
                    Id = 1,
                    QuestionGroupId = 1,
                    QuestionsDtos = questionDtos
                };

                var service = new Surveys.ServiceLayer.SurveyServices.AddQuestionService(context, new MapQuestionDtoToQuestionsService());
                var result = service.AddQuestion(surveyDto);
                result.ShouldNotBeNull();
                result.First().ErrorMessage.ShouldEqual("A question has been submitted with an invalid question type.");
                context.SaveChanges();
                context.Questions.Count().ShouldEqual(currentQuestions);
            }
        }

        [Fact]
        public void TestAddQuestionQuestionTextNotSpecified()
        {
            var options = SqliteInMemory.CreateOptions<SurveyDbContext>();
            using (var context = new SurveyDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SeedDataBaseWithSurveys();

                var currentQuestions = context.Questions.Count();
                var questionDtos = new List<QuestionDto>
                {
                    new QuestionDto
                    {
                        Type = 1
                    }
                };

                var surveyDto = new SurveyDto
                {
                    Id = 1,
                    QuestionGroupId = 1,
                    QuestionsDtos = questionDtos
                };

                var service = new Surveys.ServiceLayer.SurveyServices.AddQuestionService(context, new MapQuestionDtoToQuestionsService());
                var result = service.AddQuestion(surveyDto);
                result.ShouldNotBeNull();
                result.First().ErrorMessage.ShouldEqual("A question has been submitted without any text.");
                context.SaveChanges();
                context.Questions.Count().ShouldEqual(currentQuestions);
            }
        }



    }
}
