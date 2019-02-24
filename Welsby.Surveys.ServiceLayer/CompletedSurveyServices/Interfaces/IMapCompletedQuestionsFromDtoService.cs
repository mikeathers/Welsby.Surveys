using System;
using System.Collections.Generic;
using System.Text;
using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.DataLayer.Models;
using Welsby.Surveys.Dtos;

namespace Welsby.Surveys.ServiceLayer.CompletedSurveyServices.Interfaces
{
    public interface IMapCompletedQuestionsFromDtoService
    {
        ICollection<CompletedQuestion> MapQuestionsFromDto(IEnumerable<CompletedQuestionDto> questionsDto, SurveyDbContext context);
    }
}
