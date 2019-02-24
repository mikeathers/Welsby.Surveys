using System.Collections.Generic;
using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.DataLayer.Models;
using Welsby.Surveys.Dtos;

namespace Welsby.Surveys.ServiceLayer.SurveyServices.Interfaces
{
    public interface IMapQuestionDtoToQuestionsService
    {
        ICollection<Question> Map(ICollection<QuestionDto> questions, SurveyDbContext context);
    }
}
