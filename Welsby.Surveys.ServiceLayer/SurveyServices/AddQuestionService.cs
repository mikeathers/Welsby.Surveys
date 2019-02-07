using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.DataLayer.Models;
using Welsby.Surveys.GenericInterfaces.GenericInterfaces;
using Welsby.Surveys.ServiceLayer.SurveyServices.Dtos;
using Welsby.Surveys.ServiceLayer.SurveyServices.Interfaces;

namespace Welsby.Surveys.ServiceLayer.SurveyServices
{
    public class AddQuestionService : IAddQuestionService
    {
        private readonly SurveyDbContext _context;
        private readonly IMapQuestionDtoToQuestionsService _questionMapper;

        public StatusGenericHandler Status { get; private set; } = new StatusGenericHandler();

        public AddQuestionService(SurveyDbContext context, IMapQuestionDtoToQuestionsService questionMapper)
        {
            _context = context;
            _questionMapper = questionMapper;
        }

        public IImmutableList<ValidationResult> AddQuestion(SurveyDto surveyDto)
        {
            var survey = _context.Find<Survey>(surveyDto.Id);
            if (survey == null)
            {
                Status.AddError($"Could not find Survey with an Id of {surveyDto.Id}");
                return Status.Errors;
            }

            var questionGroup = _context.Find<QuestionGroup>(surveyDto.QuestionGroupId);
            if (questionGroup == null)
            {
                Status.AddError($"Could not find QuestionGroup with an Id of {surveyDto.QuestionGroupId}");
                return Status.Errors;
            }

            var questions = _questionMapper.Map(surveyDto.QuestionsDtos);
            
            Status = survey.AddQuestions(questions, questionGroup, _context);
            if (Status.HasErrors) return Status.Errors;
            _context.SaveChanges();
            return null;
            

        }
    }

}