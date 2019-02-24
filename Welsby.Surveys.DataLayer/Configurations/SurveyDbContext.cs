using Microsoft.EntityFrameworkCore;
using Welsby.Surveys.DataLayer.Configurations.ModelConfigs;
using Welsby.Surveys.DataLayer.Models;

namespace Welsby.Surveys.DataLayer.Configurations
{
    public class SurveyDbContext : DbContext
    {
        public SurveyDbContext() { }

        public SurveyDbContext(DbContextOptions<SurveyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SurveyConfig());
            modelBuilder.ApplyConfiguration(new QuestionGroupConfig());
        }



        public DbSet<Survey> Surveys { get; set; }
        public DbSet<QuestionGroup> QuestionGroups { get; set; }
        public DbSet<CompletedSurvey> CompletedSurveys { get; set; }
        public DbSet<CompletedQuestion> CompletedQuestions { get; set; }
        public DbSet<QuestionType> QuestionTypes { get; set; }
        public DbSet<Question> Questions { get; set; }
    }
}
