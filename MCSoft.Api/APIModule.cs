using MCSoft.Application;
using MCSoft.Infrastructure;
using MCSoft.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.ReDoc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace MCSoft.Api
{
    [DependsOn(typeof(AbpAspNetCoreMvcModule),
             typeof(MCSoftApplicationModule),
             typeof(MCSoftInfrastructureModule),
             typeof(AbpAutofacModule),
             typeof(AbpAutoMapperModule))]
    public class APIModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            ConfigureAutoMapper();

            AuthConfigurer.Configure(context.Services, configuration);

            ConfigureSwaggerServices(context.Services);
        }


        private void ConfigureAutoMapper()
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<APIModule>();

            });
        }

        private void ConfigureSwaggerServices(IServiceCollection services)
        {
            var path = System.IO.Directory.GetCurrentDirectory();
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "MCSoft API", Version = "v1" });
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



            //app.UseMvcWithDefaultRouteAndArea();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "defaultWithArea",
            //        template: "api/{area}/{controller=Home}/{action=Index}/{id?}");

            //    routes.MapRoute(
            //        name: "default",
            //        template: "api/{controller=Home}/{action=Index}/{id?}");
            //});

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
