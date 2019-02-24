using System.Collections.Generic;

namespace Welsby.Surveys.Dtos
{
    public class SurveyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NewName { get; set; }
        public ICollection<QuestionDto> QuestionsDtos { get; set; }
        public ICollection<QuestionGroupDto> QuestionGroupsDtos { get; set; }
        public int QuestionTypeId { get; set; }
        public int QuestionGroupId { get; set; }

        public string QuestionText { get; set; }
    }
}
