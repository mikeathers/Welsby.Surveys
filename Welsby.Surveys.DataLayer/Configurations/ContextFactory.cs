using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Welsby.Surveys.AppSettings;

namespace Welsby.Surveys.DataLayer.Configurations
{
    class ContextFactory : IDesignTimeDbContextFactory<SurveyDbContext>
    {
        private readonly IConfiguration _configuration;
        private readonly IAppSettings _appSettings;

        public ContextFactory(IConfiguration configuration, IAppSettings appSettings)
        {
            _configuration = configuration;
            _appSettings = appSettings;
        }

        public ContextFactory()
        {
        }

        public SurveyDbContext CreateDbContext(string[] args)
        {
            //const string connectionString =
            //    "Data Source=LAPTOP-NR5UK36Q;Initial Catalog=Welsby.Surveys;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var configuration = _appSettings.GetConfiguration();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var optionsBuilder = new DbContextOptionsBuilder<SurveyDbContext>();
            optionsBuilder.UseSqlServer(connectionString, m => m.MigrationsAssembly("Welsby.Surveys.DataLayer"));
            return new SurveyDbContext(optionsBuilder.Options);
        }
    }
}