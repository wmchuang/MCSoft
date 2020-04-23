using MCSoft.Application.Dto.Head;
using MCSoft.Application.Dto.User;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace MCSoft.Application.Dto.Evaluate
{
    public class EvaluateDto : CreationAuditedEntityDto<Guid>
    {
        public Guid UserId { get; set; }

        public Guid HeadId { get; set; }

        /// <summary>
        /// 评价内容
        /// </summary>
        public string Content { get; set; }

        public virtual UserSimpleDto User { get; set; }

        public virtual HeadSimpleDto Head { get; set; }
    }
}
