using MCSoft.Application;
using MCSoft.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace MCSoft
{
    [DependsOn(typeof(AbpAspNetCoreMvcModule),
               typeof(MCSoftApplicationModule),
               typeof(MCSoftInfrastructureModule),
               typeof(AbpAutofacModule),
               typeof(AbpAutoMapperModule))]
    public class AppModule : AbpModule
    {
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Index");
            }

            app.UseStaticFiles();


            app.UseRouting();

            app.UseAuthentication();//身份认证中间件
            app.UseAuthorization(); //身份授权


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            //app.UseMvcWithDefaultRouteAndArea();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "Default",
            //        template: "{controller}/{action}/{id?}",
            //        defaults: new { controller = "Home", action = "Index" }
            //    );
            //});
        }
    }
}