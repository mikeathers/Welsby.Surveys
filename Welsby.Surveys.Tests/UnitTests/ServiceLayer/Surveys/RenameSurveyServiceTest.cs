using System.Linq;
using TestSupport.EfHelpers;
using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.Dtos;
using Welsby.Surveys.ServiceLayer.SurveyServices;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace Welsby.Surveys.Tests.UnitTests.ServiceLayer.Surveys
{
    public class RenameSurveyServiceTest
    {
        [Fact]
        public void ShouldRenameASurvey()
        {
            var options = SqliteInMemory.CreateOptions<SurveyDbContext>();
            using (var context = new SurveyDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SeedDataBaseWithSurveys();

                var surveyDto = new SurveyDto
                {
                    Id = 1,
                    NewName = "Test New Name"
                };

                var service = new RenameSurveyService(context);

                var result = service.RenameSurvey(surveyDto);
                result.ShouldBeNull();
                context.SaveChanges();
                context.Surveys.First(m => m.SurveyId == surveyDto.Id).Name.ShouldEqual(surveyDto.NewName);
            }
        }

        [Fact]
        public void ShouldReturnAnErrorMessageIfSurveyNotFound()
        {
            var options = SqliteInMemory.CreateOptions<SurveyDbContext>();
            using (var context = new SurveyDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SeedDataBaseWithSurveys();

                var surveyDto = new SurveyDto
                {
                    Id = -1,
                    NewName = "Test New Name"
                };

                var service = new RenameSurveyService(context);

                var result = service.RenameSurvey(surveyDto);
                result.ShouldNotBeNull();
                result.First().ErrorMessage.ShouldEqual($"A survey with the Id {surveyDto.Id} does not exist.");
            }
        }

        [Fact]
        public void ShouldReturnAnErrorMessageIfNewNameNotProvided()
        {
            var options = SqliteInMemory.CreateOptions<SurveyDbContext>();
            using (var context = new SurveyDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SeedDataBaseWithSurveys();

                var currentName = context.Surveys.First(m => m.SurveyId == 1).Name;

                var surveyDto = new SurveyDto
                {
                    Id = 1,
                    NewName = ""
                };

                var service = new RenameSurveyService(context);

                var result = service.RenameSurvey(surveyDto);
                result.ShouldNotBeNull();
                result.First().ErrorMessage.ShouldEqual("No new name has been provided for this survey.");
                context.SaveChanges();
                context.Surveys.First(m => m.SurveyId == surveyDto.Id).Name.ShouldEqual(currentName);
            }
        }
    }
}
