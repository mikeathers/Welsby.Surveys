using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.DataLayer.Models;
using Welsby.Surveys.Dtos;
using Welsby.Surveys.GenericInterfaces.GenericInterfaces;
using Welsby.Surveys.ServiceLayer.SurveyServices.Interfaces;

namespace Welsby.Surveys.ServiceLayer.SurveyServices
{
    public class RemoveSurveyService : IRemoveSurveyService
    {
        private readonly SurveyDbContext _context;

        public StatusGenericHandler Status { get; set; } = new StatusGenericHandler();

        public RemoveSurveyService(SurveyDbContext context)
        {
            _context = context;
        }

        public IImmutableList<ValidationResult> RemoveSurvey(SurveyDto dto)
        {
            var survey = _context.Find<Survey>(dto.Id);
            
            if (survey == null)
            {
                Status.AddError($"A survey with the ID of {dto.Id} was not found.");
                return Status.Errors;
            }

            Status = survey.RemoveSelf(_context);
            if (Status.HasErrors) return Status.Errors;
            _context.SaveChanges();
            return null;

        }
    }
}
