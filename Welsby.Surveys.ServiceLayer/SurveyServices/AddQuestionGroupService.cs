using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.DataLayer.Models;
using Welsby.Surveys.Dtos;
using Welsby.Surveys.GenericInterfaces.GenericInterfaces;
using Welsby.Surveys.ServiceLayer.SurveyServices.Interfaces;

namespace Welsby.Surveys.ServiceLayer.SurveyServices
{
    public class AddQuestionGroupService : IAddQuestionGroupService
    {
        private readonly SurveyDbContext _context;
        private readonly IMapQuestionsToGroupService _questionMapper;
        public IStatusGeneric Status { get; private set; } = new StatusGenericHandler();

        public AddQuestionGroupService(SurveyDbContext context, IMapQuestionsToGroupService questionMapper)
        {
            _context = context;
            _questionMapper = questionMapper;
        }

        public IImmutableList<ValidationResult> AddQuestionGroup(SurveyDto dto)
        {
            var survey = _context.Find<Survey>(dto.Id);

            if (survey == null)
            {
                Status.AddError($"Could not find Survey with an Id of {dto.Id}");
                return Status.Errors;
            }

            var questionGroups = _questionMapper.Map(dto.QuestionGroupsDtos, _context);
            Status = survey.AddQuestionGroups(questionGroups, _context);
            if (Status.HasErrors) return Status.Errors;
            _context.SaveChanges();
            return null;
        }
    }
}
