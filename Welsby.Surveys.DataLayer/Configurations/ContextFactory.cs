using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Welsby.Surveys.AppSettings;

namespace Welsby.Surveys.DataLayer.Configurations
{
    class ContextFactory : IDesignTimeDbContextFactory<SurveyDbContext>
    {
        private readonly IConfiguration _configuration;
        private readonly IAppSettings _appSettings;
        private readonly ILoggerFactory _loggerFactory;

        public ContextFactory(IConfiguration configuration, IAppSettings appSettings, ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _appSettings = appSettings;
            _loggerFactory = loggerFactory;
        }

        public ContextFactory()
        {
        }

        public SurveyDbContext CreateDbContext(string[] args)
        {
            
            var configuration = _appSettings.GetConfiguration();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var optionsBuilder = new DbContextOptionsBuilder<SurveyDbContext>();
            optionsBuilder.UseSqlServer(connectionString, m => m.MigrationsAssembly("Welsby.Surveys.DataLayer"));
            return new SurveyDbContext(optionsBuilder.Options, _loggerFactory);
        }
    }
}