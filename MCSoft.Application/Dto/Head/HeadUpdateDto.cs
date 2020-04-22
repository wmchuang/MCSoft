using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace MCSoft.Application.Dto.Head
{
    public class HeadUpdateDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public string WxNumber { get; set; }

        /// <summary>
        /// 小区名
        /// </summary>
        public string CellName { get; set; }

        public string Remark { get; set; }

        public string Location { get; set; }

    }
}
