using MCSoft.Domain;
using MCSoft.Infrastructure.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.Modularity;

namespace MCSoft.Infrastructure
{
    [DependsOn(
       typeof(MCSoftDomainModule),
       typeof(AbpEntityFrameworkCoreMySQLModule)
       )]
    public class MCSoftInfrastructureModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<MCSoftDbContext>(options =>
            {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
                options.AddDefaultRepositories(includeAllEntities: true);

            });

            Configure<AbpDbContextOptions>(options =>
            {
                /* The main point to change your DBMS.
                  * See also BookStoreMigrationsDbContextFactory for EF Core tooling. */
                options.Configure(ctx =>
                {
                    if (ctx.ExistingConnection != null)
                    {
                        ctx.DbContextOptions.UseMySql(ctx.ExistingConnection);
                    }
                    else
                    {
                        ctx.DbContextOptions.UseMySql(ctx.ConnectionString);
                    }
                });
            });
        }

        public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
        {
            SeedTestData(context);
        }

        private static void SeedTestData(ApplicationInitializationContext context)
        {
            using var scope = context.ServiceProvider.CreateScope();
            scope.ServiceProvider
                .GetRequiredService<ContextDataBuilder>()
                .Build();
        }
    }
}
