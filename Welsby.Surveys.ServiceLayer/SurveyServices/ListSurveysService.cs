using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.DataLayer.Models;
using Welsby.Surveys.ServiceLayer.SurveyServices.Dtos;
using Welsby.Surveys.ServiceLayer.SurveyServices.Interfaces;
using Welsby.Surveys.ServiceLayer.SurveyServices.QueryObjects;

namespace Welsby.Surveys.ServiceLayer.SurveyServices
{
    public class ListSurveysService : IListSurveysService
    {
        private readonly SurveyDbContext _context;

        public ListSurveysService(SurveyDbContext context)
        {
            _context = context;
        }

        public Task<List<Survey>> GetSurveys()
        {
            if (_context == null) return null;
            return _context.Surveys.ToListAsync();
        }

        public IQueryable<SurveyListForDropdownDto> GetSurveysForDropdown()
        {
            if (_context == null) return null;
            return _context.Surveys.MapSurveyListForDropdown();
        }
    }
}