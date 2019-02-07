using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using Welsby.Surveys.ServiceLayer.SurveyServices.Dtos;

namespace Welsby.Surveys.ServiceLayer.SurveyServices.Interfaces
{
    public interface IRemoveSurveyService
    {
        IImmutableList<ValidationResult> RemoveSurvey(SurveyDto dto);
    }
}
