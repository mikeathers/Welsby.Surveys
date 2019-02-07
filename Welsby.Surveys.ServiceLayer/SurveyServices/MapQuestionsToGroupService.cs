using System.Collections.Generic;
using System.Linq;
using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.DataLayer.Models;
using Welsby.Surveys.ServiceLayer.SurveyServices.Dtos;
using Welsby.Surveys.ServiceLayer.SurveyServices.Interfaces;

namespace Welsby.Surveys.ServiceLayer.SurveyServices
{
    public class MapQuestionsToGroupService : IMapQuestionsToGroupService
    {
        private readonly SurveyDbContext _context;


        public MapQuestionsToGroupService(SurveyDbContext context)
        {
            _context = context;
        }

        public ICollection<QuestionGroup> Map(ICollection<QuestionGroupDto> questionGroupsDtos)
        {
            var questionGroups = new List<QuestionGroup>();

            foreach (var questionGroup in questionGroupsDtos)
            {
                var newQuestions = new List<Question>();

                foreach (var question in questionGroup.Questions)
                {
                    var type = _context.QuestionTypes.First(m => m.QuestionTypeId == question.Type);
                    var newQuestion = new Question(question.Text, type);
                    newQuestions.Add(newQuestion);
                }

                var newQuestionGroup = new QuestionGroup(questionGroup.Name, newQuestions);
                questionGroups.Add(newQuestionGroup);
            }

            return questionGroups;
        }
    }
}
