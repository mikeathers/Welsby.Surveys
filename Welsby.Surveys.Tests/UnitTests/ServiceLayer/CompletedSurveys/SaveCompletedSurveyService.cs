using System.Linq;
using TestSupport.EfHelpers;
using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.Dtos;
using Welsby.Surveys.ServiceLayer.CompletedSurveyServices;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace Welsby.Surveys.Tests.UnitTests.ServiceLayer
{
    public class SaveCompletedSurveyService
    {
        
        [Fact]
        public void TestSaveSurveyOk()
        {
            var options = SqliteInMemory.CreateOptions<SurveyDbContext>();
            using (var context = new SurveyDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SeedDataBaseWithSurveys();

                var currentCompletedSurveys = context.CompletedSurveys.Count();
                var completedQuestionsDto = EfTestData.CreateCompletedQuestionsDto();
                var completedSurveyDto = new CompletedSurveyDto
                {
                    Name = "Test Completed Survey",
                    CaseNo = 999,
                    Questions = completedQuestionsDto
                };

                var service = new SaveCompletedSurveysService(context, new MapCompletedQuestionsFromDtoService());
                var result = service.SaveCompletedSurvey(completedSurveyDto);
                result.ShouldBeNull();
                context.SaveChanges();
                service.Errors.Count.ShouldEqual(0);
                context.CompletedSurveys.Count().ShouldEqual(currentCompletedSurveys + 1);
            }
        }

        [Fact]
        public void TestSaveSurveyNoName()
        {
            var options = SqliteInMemory.CreateOptions<SurveyDbContext>();
            using (var context = new SurveyDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SeedDataBaseWithSurveys();

                var currentCompletedSurveys = context.CompletedSurveys.Count();
                var completedQuestionsDto = EfTestData.CreateCompletedQuestionsDto();
                var completedSurveyDto = new CompletedSurveyDto
                {
                    CaseNo = 999,
                    Questions = completedQuestionsDto
                };

                var service = new SaveCompletedSurveysService(context, new MapCompletedQuestionsFromDtoService());
                var result = service.SaveCompletedSurvey(completedSurveyDto);
                context.SaveChanges();
                result.ShouldNotBeNull();
                service.Errors.Count.ShouldEqual(1);
                service.Errors.First().ErrorMessage.ShouldEqual("A survey name has not been supplied.");
                context.CompletedSurveys.Count().ShouldEqual(currentCompletedSurveys);
            }
        }

        [Fact]
        public void TestSaveSurveyNoCaseNo()
        {
            var options = SqliteInMemory.CreateOptions<SurveyDbContext>();
            using (var context = new SurveyDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SeedDataBaseWithSurveys();

                var currentCompletedSurveys = context.CompletedSurveys.Count();
                var completedQuestionsDto = EfTestData.CreateCompletedQuestionsDto();
                var completedSurveyDto = new CompletedSurveyDto
                {
                    Name = "Test Completed Survey",
                    Questions = completedQuestionsDto
                };

                var service = new SaveCompletedSurveysService(context, new MapCompletedQuestionsFromDtoService());
                var result = service.SaveCompletedSurvey(completedSurveyDto);
                context.SaveChanges();
                result.ShouldNotBeNull();
                service.Errors.Count.ShouldEqual(1);
                service.Errors.First().ErrorMessage.ShouldEqual("Case number cannot be null.");
                context.CompletedSurveys.Count().ShouldEqual(currentCompletedSurveys);
            }
        }

        [Fact]
        public void TestSaveSurveyNoQuestions()
        {
            var options = SqliteInMemory.CreateOptions<SurveyDbContext>();
            using (var context = new SurveyDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SeedDataBaseWithSurveys();

                var currentCompletedSurveys = context.CompletedSurveys.Count();
                var completedQuestionsDto = EfTestData.CreateCompletedQuestionsDto();
                var completedSurveyDto = new CompletedSurveyDto
                {
                    Name = "Test Completed Survey",
                    CaseNo = 999
                };

                var service = new SaveCompletedSurveysService(context, new MapCompletedQuestionsFromDtoService());
                var result = service.SaveCompletedSurvey(completedSurveyDto);
                context.SaveChanges();
                result.ShouldNotBeNull();
                result.First().ErrorMessage.ShouldEqual("No Questions have been submitted with this completed survey.");
                context.CompletedSurveys.Count().ShouldEqual(currentCompletedSurveys);
            }
        }
    }
}
