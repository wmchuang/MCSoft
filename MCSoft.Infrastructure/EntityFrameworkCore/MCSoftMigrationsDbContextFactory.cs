﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
