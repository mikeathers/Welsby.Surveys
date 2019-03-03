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
            try
            {
                if (_context == null) return null;
                return _context.Surveys.ToListAsync();
            }
            catch
            {
                // Log error;
                return null;
            }

            
        }

        public IQueryable<SurveyListForDropdownDto> GetSurveysForDropdown()
        {
            try
            {
                if (_context == null) return null;
                return _context.Surveys.MapSurveyListForDropdown();
            }
            catch
            {
                // Log error;
                return null;
            }
        }
    }
}