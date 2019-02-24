using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using Welsby.Surveys.Dtos;

namespace Welsby.Surveys.ServiceLayer.SurveyServices.Interfaces
{
    public interface IAddQuestionGroupService
    {
        IImmutableList<ValidationResult> AddQuestionGroup(SurveyDto dto);
    }
}
