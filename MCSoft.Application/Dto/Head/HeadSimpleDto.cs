using MCSoft.Application.Dto.User;
using MCSoft.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace MCSoft.Application.Dto.Head
{
    public class HeadSimpleDto : EntityDto<Guid>
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        public string Phone { get; set; }

        public string WxNumber { get; set; }
    }
}
