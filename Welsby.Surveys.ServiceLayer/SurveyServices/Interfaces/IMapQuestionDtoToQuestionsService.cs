using System;
using System.Collections.Generic;
using System.Text;
using Welsby.Surveys.DataLayer.Models;
using Welsby.Surveys.ServiceLayer.SurveyServices.Dtos;

namespace Welsby.Surveys.ServiceLayer.SurveyServices.Interfaces
{
    public interface IMapQuestionDtoToQuestionsService
    {
        ICollection<Question> Map(ICollection<QuestionDto> questions);
    }
}
