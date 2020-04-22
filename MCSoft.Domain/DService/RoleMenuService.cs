using MCSoft.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace MCSoft.Domain.Service
{
    public class RoleMenuService : DomainService
    {
        #region Ctor

        private readonly IRepository<Role, Guid> _roleRepository;
        private readonly IRepository<Menu, Guid> _menuRepository;
        private readonly IRepository<RoleMenu> _roleMenuRepository;

        public RoleMenuService(IRepository<Role, Guid> roleRepository, IRepository<Menu, Guid> menuRepository, IRepository<RoleMenu> roleMenuRepository)
        {
            _roleRepository = roleRepository;
            _menuRepository = menuRepository;
            _roleMenuRepository = roleMenuRepository;
        }

        #endregion
        public IQueryable<Menu> GetRoleMenu(Role role)
        {
            var roleMenu = _roleMenuRepository.Where(x => x.RoleId == role.Id);
            var menu = from m in _menuRepository
                       join rm in roleMenu on m.Id equals rm.MenuId
                       select m;

            return menu;
        }


        /// <summary>
        /// 给角色赋予新的权限
        /// </summary>
        /// <param name="role"></param>
        /// <param name="menus"></param>
        /// <returns></returns>
        public async Task AddPermissionsToRole(Role role, List<string> menuNameList)
        {
            if (role == null)
            {
                throw new BusinessException("不存在的角色！");
            }

            //删除所有的原有权限
            await _roleMenuRepository.DeleteAsync(x => x.RoleId == role.Id);

            //赋予权限
            foreach (var menuName in menuNameList)
            {
                var menu = await _menuRepository.FindAsync(x => x.Name == menuName);
                if (menu == null) continue;
                var roleMenu = RoleMenu.CreateRoleMenu(role, menu);
                await _roleMenuRepository.InsertAsync(roleMenu);
            }
        }

    }
}
