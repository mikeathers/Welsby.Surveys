using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Welsby.Surveys.BizDbAccess.CompletedSurveys;
using Welsby.Surveys.BizLogic.CompletedSurveys;
using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.DataLayer.Models;
using Welsby.Surveys.Dtos;
using Welsby.Surveys.GenericInterfaces.GenericInterfaces;
using Welsby.Surveys.ServiceLayer.BizRunners;
using Welsby.Surveys.ServiceLayer.CompletedSurveyServices.Interfaces;

namespace Welsby.Surveys.ServiceLayer.CompletedSurveyServices
{
    public class SaveCompletedSurveysService : ISaveCompletedSurveyService
    {
        private readonly SurveyDbContext _context;
        private readonly RunnerWriteDb<SaveCompletedSurveyDto, CompletedSurvey> _runner;
        private readonly SaveCompletedSurveyAction _saveCompletedSurveyAction;
        private readonly IMapCompletedQuestionsFromDtoService _mapper;

        public IImmutableList<ValidationResult> Errors => _runner.Errors;

        public StatusGenericHandler Status { get; private set; } = new StatusGenericHandler();

        public SaveCompletedSurveysService(SurveyDbContext context, IMapCompletedQuestionsFromDtoService mapper)
        {
            _context = context;

            var saveCompletedSurveyDbAccess = new SaveCompletedSurveyDbAccess(_context);
            _saveCompletedSurveyAction = new SaveCompletedSurveyAction(saveCompletedSurveyDbAccess);
            _runner = new RunnerWriteDb<SaveCompletedSurveyDto, CompletedSurvey>(_saveCompletedSurveyAction, _context);
            _mapper = mapper;

        }

        public IImmutableList<ValidationResult> SaveCompletedSurvey(CompletedSurveyDto dto)
        {

            if (dto.Questions == null)
            {
                Status.AddError("No Questions have been submitted with this completed survey.");
                return Status.Errors;
            }

            var completedQuestions = _mapper.MapQuestionsFromDto(dto.Questions, _context);
            var completedSurveyDto = new SaveCompletedSurveyDto(dto.Name, dto.CaseNo, completedQuestions);
            _runner.RunAction(completedSurveyDto);

            return _runner.HasErrors ? _runner.Errors : null;
        }
    }
}
