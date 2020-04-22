using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace MCSoft.Application.Dto.User
{
    public class UserDto : CreationAuditedEntityDto<Guid>
    {
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

        public Guid BelongHeadId { get; set; }

    }
}
