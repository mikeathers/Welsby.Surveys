using System;
using System.Collections.Generic;
using System.Text;
using Welsby.Surveys.DataLayer.Models;
using Welsby.Surveys.ServiceLayer.SurveyServices.Dtos;

namespace Welsby.Surveys.ServiceLayer.SurveyServices.Interfaces
{
    public interface IMapQuestionsToGroupService
    {
        ICollection<QuestionGroup> Map(ICollection<QuestionGroupDto> questionGroupDtos);
    }
}
