using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Welsby.Surveys.DataLayer.Configurations
{
    class ContextFactory : IDesignTimeDbContextFactory<SurveyDbContext>
    {
        private readonly IConfiguration _configuration;

        public ContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ContextFactory()
        {
        }

        public SurveyDbContext CreateDbContext(string[] args)
        {
            const string connectionString =
                "Data Source=LAPTOP-NR5UK36Q;Initial Catalog=Welsby.Surveys;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var optionsBuilder = new DbContextOptionsBuilder<SurveyDbContext>();
            optionsBuilder.UseSqlServer(connectionString, m => m.MigrationsAssembly("Welsby.Surveys.DataLayer"));
            return new SurveyDbContext(optionsBuilder.Options);
        }
    }
}