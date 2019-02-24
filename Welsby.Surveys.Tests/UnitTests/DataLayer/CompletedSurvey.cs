using System.Collections.Generic;
using System.Linq;
using TestSupport.EfHelpers;
using Welsby.Surveys.DataLayer.Configurations;
using Welsby.Surveys.DataLayer.Models;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace Welsby.Surveys.Tests
{
    public class CompletedSurvey
    {
        [Fact]
        public void TestSqlLiteOk()
        {
            var options = SqliteInMemory.CreateOptions<SurveyDbContext>();
            using (var context = new SurveyDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SeedDataBaseWithSurveys();
                context.CompletedSurveys.Count().ShouldEqual(4);
            }
        }

        [Fact]
        public void TestAddCompletedSurvey()
        {
            var options = SqliteInMemory.CreateOptions<SurveyDbContext>();
            using (var context = new SurveyDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SeedDataBaseWithSurveys();

                var type = new QuestionType("Text");
                var question = new Question("How has your sleep pattern been?", type);
                
                var completedQuestions = new List<CompletedQuestion>
                {
                    new CompletedQuestion(question, "It's not been great.")
                };

                var completedSurvey = new DataLayer.Models.CompletedSurvey("New Test Survey", 10, completedQuestions);
                context.CompletedSurveys.Add(completedSurvey);
                context.SaveChanges();
                context.CompletedSurveys.Count().ShouldEqual(5);
            }
        }

        [Fact]
        public void TestAddCompletedQuestionToCompletedSurvey()
        {
            var options = SqliteInMemory.CreateOptions<SurveyDbContext>();
            using (var context = new SurveyDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SeedDataBaseWithSurveys();

                var completedSurvey = context.CompletedSurveys.First();
                var type = new QuestionType("Text");
                var question = new Question("How has your sleep pattern been?", type);
                var completedQuestion = new CompletedQuestion(question, "It's not been great.", completedSurvey);

                var status = completedSurvey.AddQuestion(completedQuestion, context);
                status.Errors.ShouldBeEmpty();
                status.HasErrors.ShouldBeFalse();
                status.Message.ShouldEqual("Success");
                context.SaveChanges();

                completedSurvey.CompletedQuestions.Count().ShouldEqual(2);

            }
        }
    }
}
