using MCSoft.Application.Dto.User;
using MCSoft.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace MCSoft.Application.Dto.Head
{
    public class HeadDto : CreationAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        public string Phone { get; set; }

        public string WxNumber { get; set; }

        /// <summary>
        /// 小区名
        /// </summary>
        public string CellName { get; set; }

        public string Remark { get; set; }

        public string Location { get; set; }

        public Status HeadStatus { get; set; }

        public int BrowseCount { get; set; }

        [NotMapped]
        public int FansCount { get; set; }

        public UserSimpleDto UserDto { get; set; }
    }
}
