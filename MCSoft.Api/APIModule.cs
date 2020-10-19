using System;
using MCSoft.Application;
using MCSoft.Infrastructure;
using MCSoft.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.IO;
using Microsoft.Extensions.Configuration;
using Volo.Abp;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.MultiTenancy.ConfigurationStore;

namespace MCSoft.Api
{
    [DependsOn(typeof(AbpAspNetCoreMvcModule),
        typeof(MCSoftApplicationModule),
        typeof(MCSoftInfrastructureModule),
        typeof(AbpAutofacModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpAspNetCoreMultiTenancyModule))]
    public class APIModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = true;
            });
            
            ConfigureAutoMapper();

            AuthConfigurer.Configure(context.Services, configuration);

           // Configure<AbpDefaultTenantStoreOptions>(BuildConfiguration());
            Configure<AbpDefaultTenantStoreOptions>(options =>
            {
                options.Tenants = new[]
                {
                    new TenantConfiguration(
                        Guid.Parse("446a5211-3d72-4339-9adc-845151f8ada0"), //Id
                        "tenant1" //Name
                    ),
                    new TenantConfiguration(
                        Guid.Parse("25388015-ef1c-4355-9c18-f6b6ddbaf89d"), //Id
                        "tenant2" //Name
                    )
                    {
                        //tenant2 有单独的数据库连接字符串
                        ConnectionStrings =
                        {
                            {ConnectionStrings.DefaultConnectionStringName, "Server=127.0.0.1;Port=3306;Database=mcsoft;uid=root;pwd=123456;Charset=utf8mb4"}
                        }
                    }
                };
            });
            
            ConfigureSwaggerServices(context.Services);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
        }

        private void ConfigureAutoMapper()
        {
            Configure<AbpAutoMapperOptions>(options => { options.AddMaps<APIModule>(); });
        }

        private void ConfigureSwaggerServices(IServiceCollection services)
        {
            var path = System.IO.Directory.GetCurrentDirectory();
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo {Title = "MCSoft API", Version = "v1"});
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);

                    var xmlPath = Path.Combine(path, "MCSoft.Api.xml");
                    options.IncludeXmlComments(xmlPath, true); //默认的第二个参数是false，这个是controller的注释，记得修改

                    xmlPath = Path.Combine(path, "MCSoft.Application.xml");
                    options.IncludeXmlComments(xmlPath); //默认的第二个参数是false，这个是controller的注释，记得修改

                    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.ApiKey,
                        In = ParameterLocation.Header,
                        Name = "Authorization"
                    });
                }
            );
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            app.UseRouting();

            // 先开启认证
            app.UseAuthentication();
            // 再开启授权
            app.UseAuthorization();
            //app.UseJwtTokenMiddleware();

            app.UseStaticFiles();

            app.UseErrorHandling();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "MCSoft API");
                options.IndexStream = () => GetType().Assembly
                    .GetManifestResourceStream("MCSoft.Api.wwwroot.swagger.ui.index.html"); // requires file to be added as an embedded resource
            });

            app.UseMultiTenancy();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                    name: "areas", "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

    }
}