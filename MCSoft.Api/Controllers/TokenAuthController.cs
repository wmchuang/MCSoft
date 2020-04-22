using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MCSoft.Utility.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Volo.Abp.Settings;
using Senparc.Weixin;
using MCSoft.Application.Service;
using MCSoft.Infrastructure.Result;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Sns;

namespace MCSoft.Api.Controllers
{
    /// <summary>
    /// 登录授权
    /// </summary>
    public class TokenAuthController : MCApiBaseController
    {
        public static readonly string
        WxOpenAppId = Config.SenparcWeixinSetting.WxOpenAppId; //与微信小程序后台的AppId设置保持一致，区分大小写。

        public static readonly string
            WxOpenAppSecret = Config.SenparcWeixinSetting.WxOpenAppSecret; //与微信小程序账号后台的AppId设置保持一致，区分大小写。

        private readonly UserAppService _userAppService;
        public TokenAuthController(UserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        /// <summary>
        /// 微信小程序授权获取openId接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<AuthenticateResultModel> AuthenticateLogin([FromBody] AuthenticateInput input)
        {
            var openId = string.Empty;
#if DEBUG
            openId = "testopenid";

#else
            var jsonResult = await SnsApi.JsCode2JsonAsync(WxOpenAppId, WxOpenAppSecret, input.Code);
            if (jsonResult.errcode == ReturnCode.请求成功)
            {
                openId = jsonResult.openid;
            }
#endif

            var dto = await _userAppService.Authorize(openId, input.HeadId);

            IdentityUser login = new IdentityUser
            {
                RememberMe = true,
                UserId = dto.Id.ToString(),
                Name = dto.NickName,
            };

            var accessToken =
                CreateAccessToken(CreateJwtClaims(login.CreateIdentity(JwtBearerDefaults.AuthenticationScheme)));

            return new AuthenticateResultModel
            {
                AccessToken = accessToken,
                ExpireInSeconds = (int)TimeSpan.FromDays(30).TotalSeconds,
                UserId = dto.Id.ToString()
            };
        }


        /// <summary>
        /// 微信小程序授权更新昵称和头像接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResultBase> Authenticate([FromBody] WxAuthenticateInputModel input)
        {
            await _userAppService.wxAuthorize(input.NickName, input.HeadImg);
            return ResultBase.Success();
        }

        /// <summary>
        /// 测试的授权接口
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)] //忽略接口，不在文档中显示
        public async Task<AuthenticateResultModel> AuthenticateTest([FromBody] TeatAuthenticateInput input)
        {
            var dto = await _userAppService.Authorize(input.OpenId, input.HeadId);

            IdentityUser login = new IdentityUser
            {
                RememberMe = true,
                UserId = dto.Id.ToString(),
                Name = dto.NickName,
            };

            var accessToken =
                CreateAccessToken(CreateJwtClaims(login.CreateIdentity(JwtBearerDefaults.AuthenticationScheme)));

            return new AuthenticateResultModel
            {
                AccessToken = accessToken,
                ExpireInSeconds = (int)TimeSpan.FromDays(30).TotalSeconds,
                UserId = dto.Id.ToString()
            };
        }


        private string CreateAccessToken(IEnumerable<Claim> claims, TimeSpan? expiration = null)
        {
            var now = DateTime.UtcNow;

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: ConfigurationHelper.BuildConfiguration().GetValue<string>("Authentication:JwtBearer:Issuer"),
                audience: ConfigurationHelper.BuildConfiguration().GetValue<string>("Authentication:JwtBearer:Audience"),
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromDays(30)),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ConfigurationHelper.BuildConfiguration().GetValue<string>("Authentication:JwtBearer:SecurityKey"))), SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        private static List<Claim> CreateJwtClaims(ClaimsIdentity identity)
        {
            var claims = identity.Claims.ToList();
            var nameIdClaim = claims.First(c => c.Type == ClaimTypes.NameIdentifier);

            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, nameIdClaim.Value),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64)
            });

            return claims;
        }
    }
}