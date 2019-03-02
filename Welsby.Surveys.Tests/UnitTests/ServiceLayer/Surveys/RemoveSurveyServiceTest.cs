using System.Linq;
using TestSupport.EfHelpers;
using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.Dtos;
using Welsby.Surveys.ServiceLayer.SurveyServices;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace Welsby.Surveys.Tests.UnitTests.ServiceLayer.Surveys
{
    public class RemoveSurveyServiceTest
    {
        [Fact]
        public void ShouldRemoveASurvey()
        {
            var options = SqliteInMemory.CreateOptions<SurveyDbContext>();
            using (var context = new SurveyDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SeedDataBaseWithSurveys();

                var currentSurveys = context.Surveys.Count();

                var dto = new SurveyDto
                {
                    Id = 1
                };

                var service = new RemoveSurveyService(context);

                var result = service.RemoveSurvey(dto);
                result.ShouldBeNull();
                context.SaveChanges();
                context.Surveys.Count().ShouldEqual(currentSurveys - 1);
            }
        }

        [Fact]
        public void ShouldReturnErrorMessageIfSurveyNotFound()
        {
            var options = SqliteInMemory.CreateOptions<SurveyDbContext>();
            using (var context = new SurveyDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SeedDataBaseWithSurveys();

                var currentSurveys = context.Surveys.Count();

                var surveyDto = new SurveyDto
                {
                    Id = -1
                };

                var service = new RemoveSurveyService(context);

                var result = service.RemoveSurvey(surveyDto);

                result.ShouldNotBeNull();
                result.First().ErrorMessage.ShouldEqual($"A survey with the ID of {surveyDto.Id} was not found.");
                context.SaveChanges();
                context.Surveys.Count().ShouldEqual(currentSurveys);
            }
        }


    }
}
