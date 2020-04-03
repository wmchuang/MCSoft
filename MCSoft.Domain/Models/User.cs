using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace MCSoft.Domain.Models
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User : FullAuditedAggregateRoot<Guid>
    {
        #region wx
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string HeadImg { get; set; }

        /// <summary>
        /// 微信openID
        /// </summary>
        public string WxOpenId { get; set; }


        #endregion

        public Guid HeadId { get; set; }



    }
}
