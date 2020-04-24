using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace MCSoft.Application.Dto.Product
{
    public class SmallProductDto : EntityDto<Guid>
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 封面图
        /// </summary>
        public string CoverImg { get; set; }


        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 是否上架
        /// </summary>
        public bool IsEnable { get; set; }
    }
}
