using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.DataLayer.Models;
using Welsby.Surveys.Dtos;
using Welsby.Surveys.GenericInterfaces.GenericInterfaces;
using Welsby.Surveys.ServiceLayer.SurveyServices.Interfaces;

namespace Welsby.Surveys.ServiceLayer.SurveyServices
{
    public class AddSurveyService : IAddSurveyService
    {
        private readonly SurveyDbContext _context;
        private readonly IMapQuestionsToGroupService _questionMapper;

        public StatusGenericHandler Status { get; set; } = new StatusGenericHandler();

        public AddSurveyService(SurveyDbContext context, MapQuestionsToGroupService questionMapper)
        {
            _context = context;
            _questionMapper = questionMapper;
        }

        public IImmutableList<ValidationResult> AddSurvey(SurveyDto surveyDto)
        {
            if (surveyDto.QuestionGroupsDtos == null)
            {
                Status.AddError("No question groups have been submitted with this survey.");
                return Status.Errors;
            }

            if (string.IsNullOrWhiteSpace(surveyDto.Name))
            {
                Status.AddError("A name has not been provided for this survey.");
                return Status.Errors;
            }

            var questionGroups = _questionMapper.Map(surveyDto.QuestionGroupsDtos, _context);
            var survey = new Survey(surveyDto.Name, questionGroups);

            if (Status.HasErrors) return Status.Errors;

            _context.Add(survey);
            _context.SaveChanges();
            return null;
        }
    }
}
