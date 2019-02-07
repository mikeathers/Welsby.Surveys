using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Welsby.Surveys.ServiceLayer.SurveyServices.Dtos;

namespace Welsby.Surveys.ServiceLayer.SurveyServices.Interfaces
{
    public interface IRenameSurveyService
    {
        IImmutableList<ValidationResult> RenameSurvey(SurveyDto dto);
    }
}
