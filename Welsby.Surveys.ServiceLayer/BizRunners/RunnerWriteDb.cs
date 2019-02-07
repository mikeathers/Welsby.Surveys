using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.GenericInterfaces.GenericInterfaces;

namespace Welsby.Surveys.ServiceLayer.BizRunners
{
    public class RunnerWriteDb<TIn, TOut>
    {
        private readonly IBizAction<TIn, TOut> _actionClass;
        private readonly SurveyDbContext _context;

        public IImmutableList<ValidationResult> Errors => _actionClass.Errors;
        public bool HasErrors => _actionClass.HasErrors;

        public RunnerWriteDb(IBizAction<TIn, TOut> actionClass, SurveyDbContext context)
        {
            _actionClass = actionClass;
            _context = context;
        }

        public TOut RunAction(TIn dataIn)
        {
            var result = _actionClass.Action(dataIn);
            if (!HasErrors)
                _context.SaveChanges();
            return result;
        }
    }
}
