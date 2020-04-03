using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace MCSoft.Domain.Models
{
    /// <summary>
    /// 后台角色
    /// </summary>
    public class ManagerRole : Entity
    {
        public Guid ManagerId { get; set; }

        public Guid RoleId { get; set; }

        public static ManagerRole CreateManagerRole(Manager manager, Role role)
        {
            return new ManagerRole() { ManagerId = manager.Id, RoleId = role.Id };
        }

        public override object[] GetKeys()
        {
            return new object[] { ManagerId, RoleId };
        }
    }
}
