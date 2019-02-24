using System;
using System.Collections.Generic;
using System.Text;
using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.DataLayer.Models;
using Welsby.Surveys.GenericInterfaces.GenericInterfaces;

namespace Welsby.Surveys.Tests.Mocks
{
    public interface IMockBizAction : IBizAction<int, string> { }

    public class MockBizAction : BizActionErrors, IMockBizAction
    {

        private readonly SurveyDbContext _context;

        public MockBizAction(SurveyDbContext context)
        {
            _context = context;
        }

        public string Action(int intIn)
        {
            if (intIn < 0)
                AddError("The intIn is less than zero");

            _context.QuestionTypes.Add(new QuestionType("MockBizAction"));

            return intIn.ToString();
        }
    }
}
