using System.Collections.Generic;
using System.Linq;
using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.DataLayer.Models;
using Welsby.Surveys.ServiceLayer.SurveyServices.Dtos;
using Welsby.Surveys.ServiceLayer.SurveyServices.Interfaces;

namespace Welsby.Surveys.ServiceLayer.SurveyServices
{
    public class MapQuestionDtoToQuestionsService : IMapQuestionDtoToQuestionsService
    {
        private readonly SurveyDbContext _context;

        public MapQuestionDtoToQuestionsService(SurveyDbContext context)
        {
            _context = context;
        }

        public ICollection<Question> Map(ICollection<QuestionDto> questionsDtos)
        {
            var questions = new List<Question>();
            var types = _context.QuestionTypes;
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
