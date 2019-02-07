using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.DataLayer.Models;
using Welsby.Surveys.GenericInterfaces.GenericInterfaces;
using Welsby.Surveys.ServiceLayer.SurveyServices.Dtos;
using Welsby.Surveys.ServiceLayer.SurveyServices.Interfaces;

namespace Welsby.Surveys.ServiceLayer.SurveyServices
{
    public class RenameSurveyService : IRenameSurveyService
    {
        private readonly SurveyDbContext _context;
        public StatusGenericHandler Status { get; set; } = new StatusGenericHandler();

        public RenameSurveyService(SurveyDbContext context)
        {
            _context = context;
        }

        public IImmutableList<ValidationResult> RenameSurvey(SurveyDto dto)
        {
            var survey = _context.Find<Survey>(dto.Id);

            if (survey == null)
            {
                Status.AddError($"A survey with the Id {dto.Id} does not exist.");
                return Status.Errors;
            }

            Status = survey.RenameSurvey(dto.NewName, _context);
            if (Status.HasErrors) return Status.Errors;
            _context.SaveChanges();
            return null;
        }
    }
}
