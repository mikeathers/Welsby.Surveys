using System;
using System.Linq;
using Welsby.Surveys.Dtos;
using Welsby.Surveys.Tests.Mocks;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace Welsby.Surveys.Tests.UnitTests.BizLogic
{
    public class SaveCompletedSurveyAction
    {
        [Fact]
        public void SaveCompletedSurveyOk()
        {
            // SETUP
            var mockDbA = new MockCompletedSurveyDbAccess();
            var service = new Surveys.BizLogic.CompletedSurveys.SaveCompletedSurveyAction(mockDbA);

            var completedQuestions = EfTestData.CreateCompletedQuestions();
            var caseNo = 999;
            var name = "Completed Survey Test";
            
            // ATTEMPT
            var result = service.Action(new SaveCompletedSurveyDto(name, caseNo, completedQuestions));

            // VERIFY
            service.Errors.Any().ShouldEqual(false);
            mockDbA.AddedCompletedSurvey.CaseNumber.ShouldEqual(caseNo);
            mockDbA.AddedCompletedSurvey.Name.ShouldEqual(name);
            mockDbA.AddedCompletedSurvey.CompletedQuestions.Count().ShouldEqual(completedQuestions.Count);

        }
    }
}
