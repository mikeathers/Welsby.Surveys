
using System.Collections.Generic;
using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.DataLayer.Models;
using Welsby.Surveys.GenericInterfaces.GenericInterfaces;
using Welsby.Surveys.ServiceLayer.SurveyServices.Dtos;
using Welsby.Surveys.ServiceLayer.SurveyServices.Interfaces;

namespace Welsby.Surveys.ServiceLayer.SurveyServices
{
    public class AddSurveyService : IAddSurveyService
    {
        private readonly SurveyDbContext _context;
        private readonly IMapQuestionsToGroupService _questionMapper;

        

        public AddSurveyService(SurveyDbContext context, IMapQuestionsToGroupService questionMapper)
        {
            _context = context;
            _questionMapper = questionMapper;
        }

        public void AddSurvey(SurveyDto surveyDto)
        {
            var questionGroups = _questionMapper.Map(surveyDto.QuestionGroupsDtos);
            var survey = new Survey(surveyDto.Name, questionGroups);
            _context.Add(survey);
            _context.SaveChanges();
        }
    }
}
