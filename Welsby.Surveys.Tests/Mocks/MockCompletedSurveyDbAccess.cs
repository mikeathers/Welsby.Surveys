using System.Collections.Immutable;
using Welsby.Surveys.BizDbAccess.CompletedSurveys;

namespace Welsby.Surveys.Tests.Mocks
{
    class MockCompletedSurveyDbAccess : ISaveSurveyDbAccess
    {
        public IImmutableList<DataLayer.Models.CompletedSurvey> CompletedSurveys { get; set; }

        public DataLayer.Models.CompletedSurvey AddedCompletedSurvey { get; set; }


        public MockCompletedSurveyDbAccess()
        {
            CompletedSurveys = EfTestData.CreateCompletedSurveys().ToImmutableList();
        }

        public void Add(DataLayer.Models.CompletedSurvey survey)
        {
            AddedCompletedSurvey = survey;
        }
    }
}
