using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Welsby.Surveys.BizDbAccess.CompletedSurveys;
using Welsby.Surveys.BizLogic.CompletedSurveys;
using Welsby.Surveys.BizLogic.CompletedSurveys.Dtos;
using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.DataLayer.Models;
using Welsby.Surveys.ServiceLayer.BizRunners;
using Welsby.Surveys.ServiceLayer.CompletedSurveyServices.Dtos;
using Welsby.Surveys.ServiceLayer.CompletedSurveyServices.Interfaces;

namespace Welsby.Surveys.ServiceLayer.CompletedSurveyServices
{
    public class SaveCompletedSurveysService : ISaveCompletedSurveyService
    {
        private readonly SurveyDbContext _context;
        private readonly RunnerWriteDb<SaveCompletedSurveyDto, CompletedSurvey> _runner;
        public IImmutableList<ValidationResult> Errors => _runner.Errors;

        public SaveCompletedSurveysService(SurveyDbContext context)
        {
            _context = context;

            var saveCompletedSurveyDbAccess = new SaveCompletedSurveyDbAccess(_context);
            var saveCompletedSurveyAction = new SaveCompletedSurveyAction(saveCompletedSurveyDbAccess);
            _runner = new RunnerWriteDb<SaveCompletedSurveyDto, CompletedSurvey>(saveCompletedSurveyAction, _context);
            
        }

        public IImmutableList<ValidationResult> SaveCompletedSurvey(CompletedSurveyDto dto)
        {
            var completedQuestions = MapQuestionsFromDto(dto.Questions);
            var completedSurveyDto = new SaveCompletedSurveyDto(dto.Name, dto.CaseNo, completedQuestions);
            _runner.RunAction(completedSurveyDto);

            return _runner.HasErrors ? _runner.Errors : null;
        }

        private ICollection<CompletedQuestion> MapQuestionsFromDto(IEnumerable<CompletedQuestionDto> questionsDto)
        {
            var completedQuestions = new List<CompletedQuestion>();

            foreach (var q in questionsDto)
            {
                var question = _context.Questions.Where(m => m.QuestionId == q.QuestionId)
                                                                                        .Include(m => m.QuestionType)
                                                                                        .Include(m => m.QuestionGroup)
                                                                                        .First();
                var completedQuestion = new CompletedQuestion(question, q.Answer);
                completedQuestions.Add(completedQuestion);
            }

            return completedQuestions;
        }

    }
}
