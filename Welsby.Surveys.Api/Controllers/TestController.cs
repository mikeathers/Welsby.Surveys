using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Welsby.Surveys.BizLogic.CompletedSurveys.Dtos;
using Welsby.Surveys.ServiceLayer.CompletedSurveyServices.Dtos;
using Welsby.Surveys.ServiceLayer.CompletedSurveyServices.Interfaces;
using Welsby.Surveys.ServiceLayer.SurveyServices.Dtos;
using Welsby.Surveys.ServiceLayer.SurveyServices.Interfaces;

namespace Welsby.Surveys.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost, Route("addsurvey")]
        public void AddSurvey(SurveyDto dto, [FromServices] IAddSurveyService service)
        {
           service.AddSurvey(dto);
        }

        [HttpPost, Route("addquestiongroup")]
        public JsonResult AddQuestionGroup(SurveyDto dto, [FromServices] IAddQuestionGroupService service)
        {
            var status = service.AddQuestionGroup(dto);
            return status == null ? new JsonResult("Ok") : new JsonResult(status.ToList());
        }

        [HttpPost, Route("addquestion")]
        public JsonResult AddQuestion(SurveyDto dto, [FromServices] IAddQuestionService service)
        {
            var status = service.AddQuestion(dto);
            return status == null ? new JsonResult("Ok") : new JsonResult(status.ToList());
        }

        [HttpPost, Route("removesurvey")]
        public JsonResult RemoveSurvey(SurveyDto dto, [FromServices] IRemoveSurveyService service)
        {
            var status = service.RemoveSurvey(dto);
            return status == null ? new JsonResult("Ok") : new JsonResult(status.ToList());
        }

        [HttpPost, Route("renamesurvey")]
        public JsonResult RenameSurvey(SurveyDto dto, [FromServices] IRenameSurveyService service)
        {
            var status = service.RenameSurvey(dto);
            return status == null ? new JsonResult("Ok") : new JsonResult(status.ToList());
        }

        [HttpPost, Route("savecompletedsurvey")]
        public JsonResult SaveCompletedSurvey(CompletedSurveyDto dto, [FromServices] ISaveCompletedSurveyService service)
        {
            var status = service.SaveCompletedSurvey(dto);
            return status == null ? new JsonResult("Ok") : new JsonResult(status.ToList());
        }
    }
}