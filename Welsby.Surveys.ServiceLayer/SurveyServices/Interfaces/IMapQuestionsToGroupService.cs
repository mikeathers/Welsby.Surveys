using System.Collections.Generic;
using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.DataLayer.Models;
using Welsby.Surveys.Dtos;

namespace Welsby.Surveys.ServiceLayer.SurveyServices.Interfaces
{
    public interface IMapQuestionsToGroupService
    {
        ICollection<QuestionGroup> Map(ICollection<QuestionGroupDto> questionGroupDtos, SurveyDbContext context);
    }
}
