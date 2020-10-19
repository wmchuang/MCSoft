using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MCSoft.Infrastructure.EntityFrameworkCore
{
    public class MCSoftMigrationsDbContextFactory : IDesignTimeDbContextFactory<MCSoftDbContext>
    {
        public MCSoftDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<MCSoftDbContext>()
                .UseMySql(configuration.GetConnectionString("Default"));

            return new MCSoftDbContext(builder.Options);
        }

        public static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
