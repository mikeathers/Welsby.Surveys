using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TestSupport.EfHelpers;
using Welsby.Surveys.Api.Controllers;
using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.DataLayer.Models;
using Welsby.Surveys.ServiceLayer.SurveyServices;
using Welsby.Surveys.ServiceLayer.SurveyServices.Interfaces;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace Welsby.Surveys.Tests.UnitTests.Controllers
{
    public class TestControllerTests
    {
        
        [Fact]
        public async Task ShouldReturnOkObjectResult()
        {
            var options = SqliteInMemory.CreateOptions<SurveyDbContext>();
            using (var context = new SurveyDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SeedDataBaseWithSurveys();
                var controller = new TestController();
                var surveys = EfTestData.CreateSurveys();

                var mockService = new Mock<IListSurveysService>();
                mockService.Setup(m => m.GetSurveys()).Returns((Task.FromResult((surveys))));

                var result = await controller.GetListOfSurveys(mockService.Object);

                result.ShouldNotBeNull();
                result.ShouldEqual(result as OkObjectResult);

                var r = result as OkObjectResult;

                r.Value.ShouldEqual(surveys);
            }
        }

        [Fact]
        public async Task ShouldReturnBadObjectResult()
        {
            var options = SqliteInMemory.CreateOptions<SurveyDbContext>();
            using (var context = new SurveyDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SeedDataBaseWithSurveys();
                var controller = new TestController();

                var mockService = new Mock<IListSurveysService>();
                mockService.Setup(m => m.GetSurveys()).Returns((Task.FromResult((List<Survey>)null)));

                var service = new ListSurveysService(context);
                var result = await controller.GetListOfSurveys(mockService.Object);

                result.ShouldNotBeNull();
                result.ShouldBeType<BadRequestObjectResult>();

                var r = result as BadRequestObjectResult;

                r.Value.ShouldEqual("Could not retrieve surveys.");

            }
        }
    }
}
