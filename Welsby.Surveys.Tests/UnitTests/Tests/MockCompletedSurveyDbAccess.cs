using System.Linq;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace Welsby.Surveys.Tests.UnitTests.Tests
{
    public class MockCompletedSurveyDbAccess
    {
        [Fact]
        public void TestMockDefaultCompletedSurveys()
        {
            var mock = new Mocks.MockCompletedSurveyDbAccess();

            mock.CompletedSurveys.Count.ShouldEqual(4);

            foreach (var survey in mock.CompletedSurveys)
            {
                survey.CompletedQuestions.Count().ShouldEqual(1);
                survey.CaseNumber.ShouldEqual(999);
            }
        }
    }
}
