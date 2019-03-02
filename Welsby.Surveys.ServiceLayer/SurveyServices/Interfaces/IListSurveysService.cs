using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Welsby.Surveys.DataLayer.Models;
using Welsby.Surveys.ServiceLayer.SurveyServices.Dtos;

namespace Welsby.Surveys.ServiceLayer.SurveyServices.Interfaces
{
    public interface IListSurveysService
    {
        Task<List<Survey>> GetSurveys();

        IQueryable<SurveyListForDropdownDto> GetSurveysForDropdown();
    }
}