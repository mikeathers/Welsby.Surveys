using System.Linq;
using TestSupport.EfHelpers;
using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.ServiceLayer.BizRunners;
using Welsby.Surveys.Tests.Mocks;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace Welsby.Surveys.Tests.UnitTests.Tests
{
    public class RunnerWriteDb
    {
        [Theory]
        [InlineData(1, false)]
        [InlineData(-1, true)]
        public void RunAction(int input, bool hasErrors)
        {
            var options = SqliteInMemory.CreateOptions<SurveyDbContext>();
            using (var context = new SurveyDbContext(options))
            {
                context.Database.EnsureCreated();

                var action = new MockBizAction(context);
                var runner = new RunnerWriteDb<int, string>(action, context);

                var output = runner.RunAction(input);

                output.ShouldEqual(input.ToString());
                runner.HasErrors.ShouldEqual(hasErrors);
                context.QuestionTypes.Count().ShouldEqual(hasErrors ? 0 : 1);
            }
        }
    }
}
