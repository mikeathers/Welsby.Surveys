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
    public class AddQuestionGroupService
    {
        [Fact]
        public void TestAddQuestionGroupOk()
        {
            var options = SqliteInMemory.CreateOptions<SurveyDbContext>();
            using (var context = new SurveyDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SeedDataBaseWithSurveys();

                var currentQuestionGroups = context.QuestionGroups.Count();
                var questions = EfTestData.CreateQuestionsDtos();

                var questionGroupsDto = new List<QuestionGroupDto>
                {
                    new QuestionGroupDto
                    {
                        Name = "Test QuestionGroup Dto",
                        Questions = questions
                    }
                };

                var surveyDto = new SurveyDto
                {
                    Id = 1,
                    QuestionGroupsDtos = questionGroupsDto
                };

                var service = new Surveys.ServiceLayer.SurveyServices.AddQuestionGroupService(context, new MapQuestionsToGroupService());
                var result = service.AddQuestionGroup(surveyDto);
                result.ShouldBeNull();
                context.SaveChanges();
                context.QuestionGroups.Count().ShouldEqual(currentQuestionGroups + 1);
            }
        }

        [Fact]
        public void TestAddQuestionGroupSurveyNotFound()
        {
            var options = SqliteInMemory.CreateOptions<SurveyDbContext>();
            using (var context = new SurveyDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SeedDataBaseWithSurveys();

                var currentQuestionGroups = context.QuestionGroups.Count();
                var questions = EfTestData.CreateQuestionsDtos();
                var surveyId = -1;

                var questionGroupsDto = new List<QuestionGroupDto>
                {
                    new QuestionGroupDto
                    {
                        Name = "Test QuestionGroup Dto",
                        Questions = questions
                    }
                };

                var surveyDto = new SurveyDto
                {
                    Id = surveyId,
                    QuestionGroupsDtos = questionGroupsDto
                };

                var service = new Surveys.ServiceLayer.SurveyServices.AddQuestionGroupService(context, new MapQuestionsToGroupService());
                var result = service.AddQuestionGroup(surveyDto);
                result.ShouldNotBeNull();
                context.SaveChanges();
                result.First().ErrorMessage.ShouldEqual($"Could not find Survey with an Id of {surveyId}");
                context.QuestionGroups.Count().ShouldEqual(currentQuestionGroups);
            }
        }

        [Fact]
        public void TestAddQuestionGroupNoName()
        {
            var options = SqliteInMemory.CreateOptions<SurveyDbContext>();
            using (var context = new SurveyDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SeedDataBaseWithSurveys();

                var currentQuestionGroups = context.QuestionGroups.Count();
                var questions = EfTestData.CreateQuestionsDtos();

                var questionGroupsDto = new List<QuestionGroupDto>
                {
                    new QuestionGroupDto
                    {
                        Questions = questions
                    }
                };

                var surveyDto = new SurveyDto
                {
                    Id = 1,
                    QuestionGroupsDtos = questionGroupsDto
                };

                var service = new Surveys.ServiceLayer.SurveyServices.AddQuestionGroupService(context, new MapQuestionsToGroupService());
                var result = service.AddQuestionGroup(surveyDto);
                result.ShouldNotBeNull();
                context.SaveChanges();
                result.First().ErrorMessage.ShouldEqual("A name is needed when creating a new question group.");
                context.QuestionGroups.Count().ShouldEqual(currentQuestionGroups);
            }
        }
    }
}
