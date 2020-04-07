using MCSoft.Application;
using MCSoft.Infrastructure;
using MCSoft.Utility.Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.ReDoc;
using System;
using System.Collections.Generic;
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
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "MCSoft API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);

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

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "MCSoft API");
                options.IndexStream = () => GetType().Assembly
                     .GetManifestResourceStream("MCSoft.Api.wwwroot.swagger.ui.index.html"); // requires file to be added as an embedded resource
            });


            app.UseStaticFiles();

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

        //private void ConfigureTokenAuth(ServiceConfigurationContext context)
        //{
        //    context.Services.OnRegistred<TokenAuthConfiguration>();
        //    var tokenAuthConfig = IocManager.Resolve<TokenAuthConfiguration>();

        //    tokenAuthConfig.SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appConfiguration["Authentication:JwtBearer:SecurityKey"]));
        //    tokenAuthConfig.Issuer = _appConfiguration["Authentication:JwtBearer:Issuer"];
        //    tokenAuthConfig.Audience = _appConfiguration["Authentication:JwtBearer:Audience"];
        //    tokenAuthConfig.SigningCredentials = new SigningCredentials(tokenAuthConfig.SecurityKey, SecurityAlgorithms.HmacSha256);
        //    var expirationDays = double.Parse(_appConfiguration["Authentication:JwtBearer:ExpirationDays"]);
        //    tokenAuthConfig.Expiration = TimeSpan.FromDays(expirationDays);
        //}

    }
}
