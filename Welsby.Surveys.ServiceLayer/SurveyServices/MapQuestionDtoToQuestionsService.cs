using System.Collections.Generic;
using System.Linq;
using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.DataLayer.Models;
using Welsby.Surveys.Dtos;
using Welsby.Surveys.ServiceLayer.SurveyServices.Interfaces;

namespace Welsby.Surveys.ServiceLayer.SurveyServices
{
    public class MapQuestionDtoToQuestionsService : IMapQuestionDtoToQuestionsService
    {
        public ICollection<Question> Map(ICollection<QuestionDto> questionsDtos, SurveyDbContext context)
        {
            var questions = new List<Question>();
            var types = context.QuestionTypes;
            foreach (var q in questionsDtos)
            {
                var type = types.First(m => m.QuestionTypeId == q.Type);
                var question = new Question(q.Text, type);
                questions.Add(question);
            }

            return questions;
        }
    }
}
