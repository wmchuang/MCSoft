using System;
using System.Collections.Generic;
using System.Linq;

namespace MCSoft.Utility.Auth
{
    public class AuthenticateResultModel
    {
        /// <summary>
        /// 用户token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// 用户加密token
        /// </summary>
       // public string EncryptedAccessToken { get; set; }

        /// <summary>
        /// 过去时间
        /// </summary>
        public int ExpireInSeconds { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }

        //public bool IsBind { get; set; }

        /// <summary>
        /// 是否绑定手机号
        /// </summary>
        public bool IsBindPhone { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string HeadImg { get; set; }
    }
}
