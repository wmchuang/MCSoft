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
        public decimal Price { get; set; } = 0m;

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

        public Product(string name,string shortName,string coverImg,string contentImg,decimal price,string label)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrEmpty(shortName, nameof(shortName));
            Check.NotNullOrEmpty(label, nameof(label));

            if (price < 0.01m)
            {
                throw new ArgumentException("价格不可小于0.01");
            }

            this.Name = name;
            this.CoverImg = coverImg;
            this.ContentImg = contentImg;
            this.Price = price;
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
