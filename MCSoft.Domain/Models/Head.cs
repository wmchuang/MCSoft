using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace MCSoft.Domain.Models
{
    /// <summary>
    /// 团长
    /// </summary>
    public partial class Head : FullAuditedAggregateRoot<Guid>
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

        public Status HeadStatus { get; set; } = Status.Enable;


        public int BrowseCount { get; set; } = 0;

        public virtual User User { get; set; }
    }

    public partial class Head
    {
        private Head() { }

        public Head(User user, string name, string phone, string wxNumber, string cellName)
        {
            this.User = user;
            this.Name = name;
            this.Phone = phone;
            this.WxNumber = wxNumber;
            this.CellName = cellName;
        }

        public void ChangeStatus(Status headStatus)
        {
            this.HeadStatus = headStatus;
        }

        public void AddBrowseCount()
        {
            this.BrowseCount += 1;
        }
    }
}
