using System.Linq;
using TestSupport.EfHelpers;
using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.Dtos;
using Welsby.Surveys.ServiceLayer.SurveyServices;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace Welsby.Surveys.Tests.UnitTests.ServiceLayer.Surverys
{
    public class AddSurveyService        
    {
        
        [Fact]
        public void TestAddSurveyOk()
        {
            var options = SqliteInMemory.CreateOptions<SurveyDbContext>();
            using (var context = new SurveyDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SeedDataBaseWithSurveys();

                var currentSurveys = context.Surveys.Count();
                var questionGroups = EfTestData.CreateQuestionGroupDtos();

                var surveyDto = new SurveyDto
                {
                    QuestionGroupsDtos = questionGroups,
                    Name = "Test Survey A"
                };

                var service = new Surveys.ServiceLayer.SurveyServices.AddSurveyService(context, new MapQuestionsToGroupService());
                var result = service.AddSurvey(surveyDto);
                result.ShouldBeNull();
                context.SaveChanges();
                context.Surveys.Count().ShouldEqual(currentSurveys + 1);
            }
        }

        [Fact]
        public void TestAddSurveyNoQuestionGroups()
        {
            var options = SqliteInMemory.CreateOptions<SurveyDbContext>();
            using (var context = new SurveyDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SeedDataBaseWithSurveys();

                var currentSurveys = context.Surveys.Count();
                var questionGroups = EfTestData.CreateQuestionGroupDtos();

                var surveyDto = new SurveyDto
                {
                    Name = "Test Survey A"
                };

                var service = new Surveys.ServiceLayer.SurveyServices.AddSurveyService(context, new MapQuestionsToGroupService());
                var result = service.AddSurvey(surveyDto);
                result.ShouldNotBeNull();
                context.SaveChanges();
                result.First().ErrorMessage.ShouldEqual("No question groups have been submitted with this survey.");
                context.Surveys.Count().ShouldEqual(currentSurveys);
            }
        }

        [Fact]
        public void TestAddSurveyNoName()
        {
            var options = SqliteInMemory.CreateOptions<SurveyDbContext>();
            using (var context = new SurveyDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SeedDataBaseWithSurveys();

                var currentSurveys = context.Surveys.Count();
                var questionGroups = EfTestData.CreateQuestionGroupDtos();

                var surveyDto = new SurveyDto
                {
                    QuestionGroupsDtos = questionGroups,
                };

                var service = new Surveys.ServiceLayer.SurveyServices.AddSurveyService(context, new MapQuestionsToGroupService());
                var result = service.AddSurvey(surveyDto);
                result.ShouldNotBeNull();
                context.SaveChanges();
                result.First().ErrorMessage.ShouldEqual("A name has not been provided for this survey.");
                context.Surveys.Count().ShouldEqual(currentSurveys);
            }
        }
    }
}
