using System.Collections.Generic;

namespace Welsby.Surveys.ServiceLayer.SurveyServices.Dtos
{
    public class QuestionGroupDto
    {
        public List<QuestionDto> Questions;

        public string Name { get; set; }

        public int SurveyId { get; set; }
    }
}