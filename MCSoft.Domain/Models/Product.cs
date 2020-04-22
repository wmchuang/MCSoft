using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace MCSoft.Domain.Models
{
    /// <summary>
    /// 商品
    /// </summary>
    public partial class Product : FullAuditedAggregateRoot<Guid>
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
        /// 二级Id
        /// </summary>
        public Guid? TwoCategoryId { get; set; }

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
        public bool IsEnable { get; set; } = true;

        /// <summary>
        /// 标签
        /// </summary>
        public string Label { get; set; }

  
    }

    public partial class Product
    {

        protected Product()
        {

        }

        public Product(string name,string shortName,string coverImg,string contentImg,decimal oldPrice,decimal price,uint stock,Guid categoryId,string label)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrEmpty(shortName, nameof(shortName));
            Check.NotNullOrEmpty(label, nameof(label));

            if (price < 0.01m)
            {
                throw new BusinessException("价格不可小于0.01");
            }

            if (oldPrice < 0.01m)
            {
                throw new BusinessException("原价不可小于0.01");
            }

            if (stock < 0)
            {
                throw new BusinessException("库存不可小于0");
            }

            if (categoryId == Guid.Empty)
            {
                throw new BusinessException("应对商品指定具体的类别");
            }

            this.Name = name;
            this.ShortName = shortName;
            this.CoverImg = coverImg;
            this.ContentImg = contentImg;
            this.OldPrice = oldPrice;
            this.Price = price;
            this.Stock = stock;
            this.CategoryId = categoryId;
            this.Label = label;
        }



        public void Disable()
        {
            this.IsEnable = false;
        }

        public void Enable()
        {
            this.IsEnable = true;
        }

    }
}
