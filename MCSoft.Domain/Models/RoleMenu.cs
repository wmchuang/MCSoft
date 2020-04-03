using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace MCSoft.Domain.Models
{
    public class RoleMenu : Entity
    {
        public Guid RoleId { get; set; }

        public Guid MenuId { get; set; }

        public static RoleMenu CreateRoleMenu(Role role, Menu menu)
        {
            return new RoleMenu() { RoleId = role.Id, MenuId = menu.Id };
        }

        public override object[] GetKeys()
        {
            return new object[] { RoleId, MenuId };
        }
    }
}
