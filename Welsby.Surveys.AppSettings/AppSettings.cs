using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;

namespace Welsby.Surveys.AppSettings
{
    public interface IAppSettings
    {
        IConfigurationRoot GetConfiguration();
    }

    public class AppSettings : IAppSettings
    {
        
        public IConfigurationRoot GetConfiguration()
        {
            var host = new ApplicationEnvironment();
            var hostName = host.ApplicationName;
            var nameOfProject = hostName.Substring(0, hostName.LastIndexOf("."));
            var assembly = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var parentDir = assembly.Substring(0, assembly.IndexOf($"\\{hostName}"));
            var appSettingsDir = $"{parentDir}\\{nameOfProject}.AppSettings";

           

            var builder = new ConfigurationBuilder()
                .SetBasePath(appSettingsDir)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddEnvironmentVariables();
            return builder.Build();
        }
    }
}
