
using Welsby.Surveys.BizLogic.CompletedSurveys.Dtos;
using Welsby.Surveys.DataLayer.Models;
using Welsby.Surveys.GenericInterfaces.GenericInterfaces;

namespace Welsby.Surveys.BizLogic.CompletedSurveys.Interfaces
{
    internal interface ISaveCompletedSurveyAction : IBizAction<SaveCompletedSurveyDto, CompletedSurvey> { }

}
