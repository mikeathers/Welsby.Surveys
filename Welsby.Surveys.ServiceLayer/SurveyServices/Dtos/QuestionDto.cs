using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Welsby.Surveys.ServiceLayer.SurveyServices.Dtos
{
    public class QuestionDto
    {
        public string Text { get; set; }
        public int Type { get; set; }
        public int QuestionGroupId { get; set; }

    }
}