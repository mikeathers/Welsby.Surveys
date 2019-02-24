using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.DataLayer.Models;

namespace Welsby.Surveys.BizDbAccess.CompletedSurveys
{
    public interface ISaveSurveyDbAccess
    {
        void Add(CompletedSurvey survey);
    }

    public class SaveCompletedSurveyDbAccess : ISaveSurveyDbAccess
    {
        private readonly SurveyDbContext _context;

        public SaveCompletedSurveyDbAccess(SurveyDbContext context)
        {
            _context = context;
        }

        public void Add(CompletedSurvey survey)
        {
            _context.CompletedSurveys.Add(survey);
        }
    }
}
