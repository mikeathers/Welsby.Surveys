using System.Collections.Generic;

namespace Welsby.Surveys.Dtos
{
    public class CompletedSurveyDto
    {
        public int CompletedSurveyId { get; set; }
        public string Name { get; set; }
        public int CaseNo { get; set; }
        public ICollection<CompletedQuestionDto> Questions { get; set; }
    }
}
