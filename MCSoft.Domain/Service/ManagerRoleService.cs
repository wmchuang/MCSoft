using MCSoft.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Users;

namespace MCSoft.Domain.Service
{
    public class ManagerRoleService : DomainService
    {
        #region Ctor
        private readonly IRepository<Manager, Guid> _managerRepository;
        private readonly IRepository<ManagerRole> _managerRoleRepository;
        private readonly IRepository<Role, Guid> _roleRepository;
        private readonly IRepository<Menu, Guid> _menuRepository;
        private readonly IRepository<RoleMenu> _roleMenuRepository;
        protected ICurrentUser _currentUser { get; }
        public ManagerRoleService(IRepository<ManagerRole> managerRoleRepository, IRepository<Role, Guid> roleRepository, ICurrentUser currentUser, IRepository<Manager, Guid> managerRepository, IRepository<RoleMenu> roleMenuRepository, IRepository<Menu, Guid> menuRepository)
        {

            _managerRepository = managerRepository;
            _managerRoleRepository = managerRoleRepository;
            _roleRepository = roleRepository;
            _roleMenuRepository = roleMenuRepository;
            _menuRepository = menuRepository;
            _currentUser = currentUser;
        }

        #endregion
        public IQueryable<Role> GetManagerRole(Manager manager)
        {
            var managerRoles = _managerRoleRepository.Where(x => x.ManagerId == manager.Id);
            var role = from mr in managerRoles
                       join r in _roleRepository on mr.RoleId equals r.Id
                       select r;

            var m = role.ToList();

            return role;
        }


        public async Task<IEnumerable<Menu>> GetCurrentManagerMenuAsync()
        {
            var manager = await _managerRepository.GetAsync(x => x.Id == _currentUser.Id.Value);

            var managerRoles = _managerRoleRepository.Where(x => x.ManagerId == manager.Id).ToList();
            var role = (from mr in managerRoles
                        join r in _roleRepository on mr.RoleId equals r.Id
                        select r).ToList();


            var menu = from r in role
                       join rm in _roleMenuRepository on r.Id equals rm.RoleId
                       join m in _menuRepository on rm.MenuId equals m.Id
                       select m;

            return menu.OrderBy(x => x.Order).ToList().Distinct();
        }


        /// <summary>
        /// 给用户赋予新的角色
        /// </summary>
        /// <param name="role"></param>
        /// <param name="menus"></param>
        /// <returns></returns>
        public async Task AddRoleToManager(Guid guid, List<Guid> roleIdList)
        {
            var manager = await _managerRepository.GetAsync(x => x.Id == guid);

            //删除所有的原有权限
            await _managerRoleRepository.DeleteAsync(x => x.ManagerId == manager.Id);

            //赋予权限
            foreach (var roleId in roleIdList)
            {
                var role = await _roleRepository.FindAsync(x => x.Id == roleId);
                if (role == null) continue;
                var roleMenu = ManagerRole.CreateManagerRole(manager, role);
                await _managerRoleRepository.InsertAsync(roleMenu);
            }
        }
    }
}
