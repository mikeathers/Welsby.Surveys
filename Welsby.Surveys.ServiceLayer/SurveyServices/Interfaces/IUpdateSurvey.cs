using System.Collections.Generic;
using Welsby.Surveys.DataLayer.Models;

namespace Welsby.Surveys.ServiceLayer.SurveyServices.Interfaces
{
    public interface IUpdateSurvey
    {
        void Rename(string newName);

        void AddQuestionGroup(ICollection<QuestionGroup> questionGroups);

        void AddQuestion(ICollection<Question> questions);
    }
}
