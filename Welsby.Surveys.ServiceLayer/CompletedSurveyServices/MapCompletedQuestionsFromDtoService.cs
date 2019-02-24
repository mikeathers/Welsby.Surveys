using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.DataLayer.Models;
using Welsby.Surveys.Dtos;
using Welsby.Surveys.ServiceLayer.CompletedSurveyServices.Interfaces;

namespace Welsby.Surveys.ServiceLayer.CompletedSurveyServices
{
    public class MapCompletedQuestionsFromDtoService : IMapCompletedQuestionsFromDtoService
    {
        public ICollection<CompletedQuestion> MapQuestionsFromDto(IEnumerable<CompletedQuestionDto> questionsDto, SurveyDbContext context)
        {
            var completedQuestions = new List<CompletedQuestion>();

            foreach (var q in questionsDto)
            {
                var question = context.Questions.Where(m => m.QuestionId == q.QuestionId)
                    .Include(m => m.QuestionType)
                    .Include(m => m.QuestionGroup)
                    .First();
                var completedQuestion = new CompletedQuestion(question, q.Answer);
                completedQuestions.Add(completedQuestion);
            }

            return completedQuestions;
        }
    }
}
