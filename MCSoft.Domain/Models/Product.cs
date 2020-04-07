using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace MCSoft.Domain.Models
{
    /// <summary>
    /// 商品
    /// </summary>
    public class Product : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 简称
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// 封面图
        /// </summary>
        public string CoverImg { get; set; }

        /// <summary>
        /// 商品图片
        /// </summary>
        public string ContentImg { get; set; }

        /// <summary>
        /// 原价
        /// </summary>
        public decimal OldPrice { get; set; } = 0m;

        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal Price { get; set; } = 0m;

        /// <summary>
        /// 库存
        /// </summary>
        public uint Stock { get; set; } = 0;

        /// <summary>
        /// 销量
        /// </summary>
        public uint SaleCounts { get; set; } = 0;

        /// <summary>
        /// 分类Id
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// 详情
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// 须知
        /// </summary>
        public string Notice { get; set; }
    }
}
