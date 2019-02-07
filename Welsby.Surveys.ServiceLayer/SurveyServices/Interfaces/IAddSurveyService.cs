using Welsby.Surveys.ServiceLayer.SurveyServices.Dtos;

namespace Welsby.Surveys.ServiceLayer.SurveyServices.Interfaces
{
    public interface IAddSurveyService
    {
        void AddSurvey(SurveyDto survey);
    }
}
