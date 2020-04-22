using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace MCSoft.Domain.Models
{
    /// <summary>
    /// 用户
    /// </summary>
    public partial class User : FullAuditedAggregateRoot<Guid>
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

        public Guid? BelongHeadId { get; set; }
    }

    public partial class User
    {
        private User() { }

        public User(string wxopenId, string nickname, string headImg, Guid headId)
        {
            this.WxOpenId = wxopenId;
            this.NickName = nickname;
            this.HeadImg = headImg;
            this.BelongHeadId = headId;
        }


        /// <summary>
        /// 更改团长
        /// </summary>
        /// <param name="headId"></param>
        public void ChangeHead(Guid headId)
        {
            this.BelongHeadId = headId;
        }
    }
}
