using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using Welsby.Surveys.Dtos;

namespace Welsby.Surveys.ServiceLayer.CompletedSurveyServices.Interfaces
{
    public interface ISaveCompletedSurveyService
    {
        IImmutableList<ValidationResult> SaveCompletedSurvey(CompletedSurveyDto dto);
    }
}
