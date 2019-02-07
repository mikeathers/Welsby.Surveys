using System.Collections.Generic;

namespace Welsby.Surveys.ServiceLayer.CompletedSurveyServices.Dtos
{
    public class CompletedSurveyDto
    {
        public string Name { get; set; }
        public int CaseNo { get; set; }
        public ICollection<CompletedQuestionDto> Questions { get; set; }
    }
}
