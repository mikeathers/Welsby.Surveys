using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Welsby.Surveys.DataLayer.Configurations.ModelConfigs;
using Welsby.Surveys.DataLayer.Models;

namespace Welsby.Surveys.DataLayer.Configurations
{
    public class SurveyDbContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public SurveyDbContext() { }

        public SurveyDbContext(DbContextOptions<SurveyDbContext> options, ILoggerFactory loggerFactory = null) : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SurveyConfig());
            modelBuilder.ApplyConfiguration(new QuestionGroupConfig());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }


        public DbSet<Survey> Surveys { get; set; }
        public DbSet<QuestionGroup> QuestionGroups { get; set; }
        public DbSet<CompletedSurvey> CompletedSurveys { get; set; }
        public DbSet<CompletedQuestion> CompletedQuestions { get; set; }
        public DbSet<QuestionType> QuestionTypes { get; set; }
        public DbSet<Question> Questions { get; set; }
    }
}
