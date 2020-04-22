using Microsoft.AspNetCore.Authentication;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MCSoft.Utility.Auth
{
    /// <summary>
    /// 用户认证基类
    /// </summary>
    [Serializable]
    public  class IdentityUser
    {
        /// <summary>
        /// 登录账号
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户标识
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 登录邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 显示姓名，比如王志强
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 记住我
        /// </summary>
        public bool RememberMe { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// 写入登录信息
        /// </summary>
        /// <returns></returns>
        public async Task<bool> WriteLoginInfo()
        {
            Identity identity = new Identity();
            return await identity.WriteLoginInfo(this);
        }

        /// <summary>
        /// 获取ClaimsIdentity
        /// </summary>
        /// <returns></returns>
        public ClaimsIdentity CreateIdentity(string authenticationType)
        {
            Identity identity = new Identity();
            return identity.CreateIdentity(this, authenticationType);
        }
    }
}
