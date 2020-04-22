using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MCSoft.Utility.Auth
{
    /// <summary>
    /// https://www.cnblogs.com/linezero/p/6801602.html
    /// </summary>
    public static class HttpContext
    {
        private static IHttpContextAccessor _accessor;

        public static Microsoft.AspNetCore.Http.HttpContext Current => _accessor.HttpContext;

        internal static void Configure(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <param name="authenticationScheme"></param>
        public static async void SignOutAsync(string authenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme)
        {
            await HttpContext.Current.SignOutAsync(authenticationScheme);
        }

        /// <summary>
        /// 判断是否已经登录
        /// </summary>
        /// <returns></returns>
        public static bool IsAuthenticated => HttpContext.Current.User.Identity.IsAuthenticated;

        public static async Task<bool> IsAuthenticatedAsync()
        {
            return await Task.Run(() => IsAuthenticated);
        }
    }

    public static class StaticHttpContextExtensions
    {
        public static void AddHttpContextAccessorExtensions(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static IApplicationBuilder UseStaticHttpContext(this IApplicationBuilder app)
        {
            var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            HttpContext.Configure(httpContextAccessor);
            return app;
        }
    }
}
