using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace MCSoft.Application.Dto.Product
{
    public class ProductDto : CreationAuditedEntityDto<Guid>
    {
        public Guid HeadId { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 封面图
        /// </summary>
        public string CoverImg { get; set; }

        /// <summary>
        /// 商品图片
        /// </summary>
        public string ContentImg { get; set; }

        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 详情
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// 须知
        /// </summary>
        public string Notice { get; set; }


        /// <summary>
        /// 是否上架
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public string Label { get; set; }
    }
}
