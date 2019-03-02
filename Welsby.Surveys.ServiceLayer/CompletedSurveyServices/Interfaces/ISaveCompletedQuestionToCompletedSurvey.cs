using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using Welsby.Surveys.Dtos;

namespace Welsby.Surveys.ServiceLayer.CompletedSurveyServices.Interfaces
{
    public interface ISaveCompletedQuestionToCompletedSurvey
    {
        IImmutableList<ValidationResult> SaveCompletedQuestion(CompletedSurveyDto dto);
    }
}
