using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.DataLayer.Models;
using Welsby.Surveys.Dtos;
using Welsby.Surveys.GenericInterfaces.GenericInterfaces;
using Welsby.Surveys.ServiceLayer.CompletedSurveyServices.Interfaces;

namespace Welsby.Surveys.ServiceLayer.CompletedSurveyServices
{
    public class SaveCompletedQuestionToCompletedSurveyService : ISaveCompletedQuestionToCompletedSurvey
    {
        private readonly SurveyDbContext _context;
        private readonly MapCompletedQuestionsFromDtoService _mapper;

        public StatusGenericHandler Status { get; set; } = new StatusGenericHandler();

        public SaveCompletedQuestionToCompletedSurveyService(SurveyDbContext context, MapCompletedQuestionsFromDtoService mapper = null)
        {
            _context = context;
            _mapper = mapper;
        }

        public IImmutableList<ValidationResult> SaveCompletedQuestion(CompletedSurveyDto dto)
        {
            if (dto.Questions == null)
            {
                Status.AddError("No Questions have been submitted with this completed survey.");
                return Status.Errors;
            }

            var completedSurvey = _context.Find<CompletedSurvey>(dto.CompletedSurveyId);

            if (completedSurvey == null)
            {
                Status.AddError($"Could not find Survey with an Id of {dto.CompletedSurveyId}");
                return Status.Errors;
            }

            var completedQuestions = _mapper.MapQuestionsFromDto(dto.Questions, _context);

            foreach (var question in completedQuestions)
            {
                Status = completedSurvey.AddQuestion(question, _context);
            }

            if (Status.HasErrors) return Status.Errors;
            _context.SaveChanges();
            return null;

        }
    }
}
