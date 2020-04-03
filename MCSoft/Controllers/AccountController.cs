using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MCSoft.Application.Service;
using MCSoft.Infrastructure.Result;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace MCSoft.Controllers
{
    public class AccountController : AbpController
    {
        private readonly ManagerAppService _managerAppService;
        public AccountController(ManagerAppService managerAppService)
        {
            _managerAppService = managerAppService;
        }

        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ResultBase> Login(string userName, string password)
        {
            var result = await _managerAppService.Login(userName, password);
            if (!result.status) return result;
            //Claim类似于身份证的某条内容，一条内容对应一条Claim.例如：民族：汉、籍贯：浙江杭州 此处用的是学校的学生证
            var claims = new List<Claim>()
           {
                 new Claim(ClaimTypes.NameIdentifier, result.data.Id.ToString()),
                 new Claim(ClaimTypes.Name,result.data.UserName)//姓名
             };

            //ClaimsIdentity 类似于身份证、学生证。它是有一条或者多条Claim组合而成。这样就是组成了一个学生证和驾照
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            //claimsPrincipal相当于一个人，你可以指定这个人持有哪些ClaimsIdentity（证件）,我指定他持有schoolIdentity、govIdentity那么他就是
            //在学校里是学生，在社会上是一名好司机
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            var authProperties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                // Refreshing the authentication session should be allowed.

                ExpiresUtc = DateTime.UtcNow.AddMinutes(30),
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

            //HttpContext上下文登录。会根据你在StartUp.cs文件中配置的services.AddAuthentication("CookiesAuth").AddCookie("CookiesAuth")进行操作
            //此处就是增加了Cookies

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);
            //HttpContext上下文登出,会清除Cookies
            //HttpContext.SignOutAsync()
            return result;
        }

        public async Task<RedirectResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return Redirect(Url.Action("Index"));
        }
    }
}