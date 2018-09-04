using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CMLearningWords.AccessToData.Context
{
    public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        //public static string Path { get; } = "Server=28a64aae-6d1d-41d0-835f-a92d0115a5e6.sqlserver.sequelizer.com;Database=db28a64aae6d1d41d0835fa92d0115a5e6;User ID=oiauckdfjbhlakrq;Password=35by4wwfTUcYE8siUycaxLjcBJv3Zh5Ui7wQRJkdUvD2qApZFZfLCsHpT85taCoK;";
        public ApplicationContextFactory() { }

        public ApplicationContext CreateDbContext(string[] args)
        {
            var appSettingsPath = args.Length != 0 && !string.IsNullOrWhiteSpace(args[0])
                ? args[0]
                : Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "CMLearningWords.WebUI");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(appSettingsPath)
                .AddJsonFile($"appsettings.json")
                .Build();

            var builderOption = new DbContextOptionsBuilder<ApplicationContext>();
            builderOption.UseSqlServer(configuration.GetConnectionString("Default"));

            return new ApplicationContext(builderOption.Options);
        }
    }
}
