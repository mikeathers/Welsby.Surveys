using System.Linq;
using TestSupport.EfHelpers;
using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.ServiceLayer.SurveyServices;
using Welsby.Surveys.ServiceLayer.SurveyServices.Dtos;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace Welsby.Surveys.Tests.UnitTests.ServiceLayer.Surveys
{
    public class ListSurveysServiceTest
    {
        [Fact]
        public async void ShouldReturnAListOfSurveys()
        {
            var options = SqliteInMemory.CreateOptions<SurveyDbContext>();
            using (var context = new SurveyDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SeedDataBaseWithSurveys();

                var currentSurveys = context.Surveys.ToList().Count;

                var service = new ListSurveysService(context);
                var result = await service.GetSurveys();
                result.ShouldNotBeNull();
                result.Count.ShouldEqual(currentSurveys);
            }
        }

        [Fact]
        public void ShouldReturnAListOfSurveysForDropdown()
        {
            var options = SqliteInMemory.CreateOptions<SurveyDbContext>();
            using (var context = new SurveyDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SeedDataBaseWithSurveys();

                var currentSurveys = context.Surveys.ToList().Count;

                var service = new ListSurveysService(context);
                var result = service.GetSurveysForDropdown();
                result.ShouldNotBeNull();
                result.First().ShouldBeType<SurveyListForDropdownDto>();
                result.Count().ShouldEqual(currentSurveys);
            }
        }

        [Fact]
        public void ShouldReturnNullIfContextIsNull()
        {
            var options = SqliteInMemory.CreateOptions<SurveyDbContext>();
            using (var context = new SurveyDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SeedDataBaseWithSurveys();

                var service = new ListSurveysService(null);
                
                var result = service.GetSurveys();
                result.ShouldBeNull();
            }
        }
    }
}
