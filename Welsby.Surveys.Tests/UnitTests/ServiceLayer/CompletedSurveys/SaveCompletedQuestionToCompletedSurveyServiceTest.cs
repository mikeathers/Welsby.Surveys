using System.Collections.Generic;
using System.Linq;
using TestSupport.EfHelpers;
using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.Dtos;
using Welsby.Surveys.ServiceLayer.CompletedSurveyServices;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace Welsby.Surveys.Tests.UnitTests.ServiceLayer.CompletedSurveys
{
    public class SaveCompletedQuestionToCompletedSurveyServiceTest
    {
        public List<CompletedQuestionDto> completedQuestionsDto = new List<CompletedQuestionDto>
        {
            new CompletedQuestionDto
            {
                QuestionId = 1,
                Answer = "Swimming"
            },
            new CompletedQuestionDto
            {
                QuestionId = 2,
                Answer = "My sleep pattern hasn't been great."
            }
        };

        [Fact]
        public void ShouldSaveCompletedSurvey()
        {
            var options = SqliteInMemory.CreateOptions<SurveyDbContext>();
            using (var context = new SurveyDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SeedDataBaseWithSurveys();

                var currentCompletedQuestions = context.CompletedQuestions.Count();

                var completedSurveyDto = new CompletedSurveyDto
                {
                    CompletedSurveyId = 1,
                    Name = "Test Completed Survey",
                    CaseNo = 999,
                    Questions = completedQuestionsDto
                };

                var service = new SaveCompletedQuestionToCompletedSurveyService(context, new MapCompletedQuestionsFromDtoService());

                var result = service.SaveCompletedQuestion(completedSurveyDto);
                result.ShouldBeNull();
                context.SaveChanges();
                context.CompletedQuestions.Count().ShouldEqual(currentCompletedQuestions + 2);
            }
        }

        [Fact]
        public void ShouldReturnErrorMessageWhenNoQuestionsProvided()
        {
            var options = SqliteInMemory.CreateOptions<SurveyDbContext>();
            using (var context = new SurveyDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SeedDataBaseWithSurveys();

                var currentCompletedQuestions = context.CompletedQuestions.Count();

                var completedSurveyDto = new CompletedSurveyDto
                {
                    CompletedSurveyId = 1,
                    Name = "Test Completed Survey",
                    CaseNo = 999
                };

                var service = new SaveCompletedQuestionToCompletedSurveyService(context, new MapCompletedQuestionsFromDtoService());

                var result = service.SaveCompletedQuestion(completedSurveyDto);
                result.ShouldNotBeNull();
                context.SaveChanges();
                result.First().ErrorMessage.ShouldEqual("No Questions have been submitted with this completed survey.");
                context.CompletedQuestions.Count().ShouldEqual(currentCompletedQuestions);
            }
        }

        [Fact]
        public void ShouldReturnErrorMessageWhenSurveyNotFound()
        {
            var options = SqliteInMemory.CreateOptions<SurveyDbContext>();
            using (var context = new SurveyDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SeedDataBaseWithSurveys();

                var currentCompletedQuestions = context.CompletedQuestions.Count();
                var completedSurveyId = -1;

                var completedSurveyDto = new CompletedSurveyDto
                {
                    CompletedSurveyId = completedSurveyId,
                    Name = "Test Completed Survey",
                    CaseNo = 999,
                    Questions = completedQuestionsDto
                };

                var service = new SaveCompletedQuestionToCompletedSurveyService(context, new MapCompletedQuestionsFromDtoService());

                var result = service.SaveCompletedQuestion(completedSurveyDto);
                result.ShouldNotBeNull();
                context.SaveChanges();
                result.First().ErrorMessage.ShouldEqual($"Could not find Survey with an Id of {completedSurveyId}");
                context.CompletedQuestions.Count().ShouldEqual(currentCompletedQuestions);

            }
        }

        [Fact]
        public void ShouldReturnErrorMessageWhenNoAnswerProvided()
        {
            var options = SqliteInMemory.CreateOptions<SurveyDbContext>();
            using (var context = new SurveyDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SeedDataBaseWithSurveys();

                var currentCompletedQuestions = context.CompletedQuestions.Count();

                var completedQuestionsDto = new List<CompletedQuestionDto>
                {
                    new CompletedQuestionDto
                    {
                        QuestionId = 1,
                    }
                };

                var completedSurveyDto = new CompletedSurveyDto
                {
                    CompletedSurveyId = 1,
                    Name = "Test Completed Survey",
                    CaseNo = 999,
                    Questions = completedQuestionsDto
                };

                var service = new SaveCompletedQuestionToCompletedSurveyService(context, new MapCompletedQuestionsFromDtoService());

                var result = service.SaveCompletedQuestion(completedSurveyDto);
                result.ShouldNotBeNull();
                context.SaveChanges();
                result.First().ErrorMessage.ShouldEqual("An answer is needed when submitting a question.");
                context.CompletedQuestions.Count().ShouldEqual(currentCompletedQuestions);

            }
        }
    }
}
