using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MCSoft.Utility.Auth
{
    public class Identity
    {
        /// <summary>
        /// 写入登录信息
        /// </summary>
        /// <param name="model">
        /// </param>
        /// <param name="authenticationScheme"></param>
        /// <returns>
        /// </returns>
        public async Task<bool> WriteLoginInfo(IdentityUser model, string authenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme)
        {
            var identity = CreateIdentity(model, authenticationScheme);
            var pricinpal = new ClaimsPrincipal(identity);
            var authProperties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                // Refreshing the authentication session should be allowed.

                ExpiresUtc = model.RememberMe ? DateTime.UtcNow.AddMinutes(10) : DateTime.UtcNow.AddMinutes(30),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. Required when setting the 
                // ExpireTimeSpan option of CookieAuthenticationOptions 
                // set with AddCookie. Also required when setting 
                // ExpiresUtc.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };
            await HttpContext.Current.SignInAsync(authenticationScheme, pricinpal, authProperties);

            //HttpContext.Current.User = pricinpal;
            return true;
        }

        /// <summary>
        /// 创建ClaimsIdentity（委托基于声明的标识）
        /// </summary>
        /// <param name="model">
        /// </param>
        /// <param name="authenticationType">
        /// </param>
        /// <returns>
        /// </returns>
        public ClaimsIdentity CreateIdentity(IdentityUser model, string authenticationType)
        {
            var identity = new ClaimsIdentity(authenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, model.Name));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, model.UserId.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Email, model.Email ?? "Email"));
            identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity"));
            identity.AddClaim(new Claim("DisplayName", model.DisplayName ?? model.Name));
            identity.AddClaim(new Claim(ClaimTypes.MobilePhone, model.Mobile ?? ""));
            identity.AddClaim(new Claim(ClaimTypes.Role, model.RoleId.ToString()));

            return identity;
        }
    }
}
