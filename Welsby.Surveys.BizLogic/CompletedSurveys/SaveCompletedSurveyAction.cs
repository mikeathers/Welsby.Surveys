using System.Collections.Generic;
using System.Linq;
using Welsby.Surveys.BizDbAccess.CompletedSurveys;
using Welsby.Surveys.BizLogic.CompletedSurveys.Dtos;
using Welsby.Surveys.DataLayer.Models;
using Welsby.Surveys.GenericInterfaces.GenericInterfaces;

namespace Welsby.Surveys.BizLogic.CompletedSurveys
{
    public class SaveCompletedSurveyAction : BizActionErrors, IBizAction<SaveCompletedSurveyDto, CompletedSurvey>
    {
        private readonly ISaveSurveyDbAccess _dbAccess;

        public SaveCompletedSurveyAction(ISaveSurveyDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public CompletedSurvey Action(SaveCompletedSurveyDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                AddError("A survey name has not been supplied.");
                return null;
            }

            if (dto.CaseNo == 0)
            {
                AddError("Case number cannot be null.");
                return null;
            }

            if (!dto.Questions.Any())
            {
                AddError("No questions have been answered.");
                return null;
            }

            var checkedQuestions = CheckAllQuestionsHaveAnswers(dto.Questions);

            var completedSurvey = new CompletedSurvey(dto.Name, dto.CaseNo, checkedQuestions);

            if (!HasErrors)
                _dbAccess.Add(completedSurvey);

            return HasErrors ? null : completedSurvey;
        }

        private IEnumerable<CompletedQuestion> CheckAllQuestionsHaveAnswers(IEnumerable<CompletedQuestion> completedQuestions)
        {
            var result = new List<CompletedQuestion>();
            foreach (var question in completedQuestions)
            {
                if (string.IsNullOrWhiteSpace(question.Answer))
                {
                    AddError($"Question: {question.Question.Text} - Doesn't have an answer");
                }
                else
                {
                    result.Add(question);
                }
            }

            return result;
        }
    }

}
