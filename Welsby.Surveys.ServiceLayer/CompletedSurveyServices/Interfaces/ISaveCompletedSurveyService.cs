using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using Welsby.Surveys.BizLogic.CompletedSurveys.Dtos;
using Welsby.Surveys.ServiceLayer.CompletedSurveyServices.Dtos;

namespace Welsby.Surveys.ServiceLayer.CompletedSurveyServices.Interfaces
{
    public interface ISaveCompletedSurveyService
    {
        IImmutableList<ValidationResult> SaveCompletedSurvey(CompletedSurveyDto dto);
    }
}
