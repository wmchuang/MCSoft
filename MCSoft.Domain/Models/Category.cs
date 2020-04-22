using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace MCSoft.Domain.Models
{
    /// <summary>
    /// 产品分类
    /// </summary>
    public partial class Category : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string Img { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public Guid? ParentId { get; set; }
    }

    public partial class Category
    {
        protected Category() { }


        public Category(string name, string img)
        {
            this.Name = name;
            this.Img = img;
        }
    }
}
