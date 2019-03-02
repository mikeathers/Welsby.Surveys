using System.Linq;
using Welsby.Surveys.DataLayer.Models;
using Welsby.Surveys.ServiceLayer.SurveyServices.Dtos;

namespace Welsby.Surveys.ServiceLayer.SurveyServices.QueryObjects
{
    public static class SurveyListForDropdown
    {
        public static IQueryable<SurveyListForDropdownDto> MapSurveyListForDropdown(this IQueryable<Survey> surveys)
        {
            return surveys.Select(m => new SurveyListForDropdownDto
            {
                Text = m.Name,
                Value = m.SurveyId
            });
        }
    }
}
