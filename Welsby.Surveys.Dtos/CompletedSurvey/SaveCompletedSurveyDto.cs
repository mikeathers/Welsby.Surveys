using System.Collections.Generic;
using Welsby.Surveys.DataLayer.Models;

namespace Welsby.Surveys.Dtos
{
    public class SaveCompletedSurveyDto
    {
        public string Name { get; set; }
        public int CaseNo { get; set; }
        public ICollection<CompletedQuestion> Questions { get; set; }

        public SaveCompletedSurveyDto(string name, int caseNo, ICollection<CompletedQuestion> questions)
        {
            Name = name;
            CaseNo = caseNo;
            Questions = questions;
        }
    }
}
