using MCSoft.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.UI.Navigation;

namespace MCSoft.Infrastructure
{
    public class ContextDataBuilder : ITransientDependency
    {
        private readonly IRepository<Menu, Guid> _repository;
        private readonly IRepository<Role, Guid> _roleRepository;
        private readonly IRepository<Manager, Guid> _managerRepository;
        private readonly IRepository<ManagerRole> _managerRoleRepository;

        public ContextDataBuilder(IRepository<Menu, Guid> repository, IRepository<Role, Guid> roleRepository, IRepository<Manager, Guid> managerRepository, IRepository<ManagerRole> managerRoleRepository)
        {
            _repository = repository;
            _roleRepository = roleRepository;
            _managerRepository = managerRepository;
            _managerRoleRepository = managerRoleRepository;
        }

        public async void Build()
        {
            await InitMenusAsync();
            await InitRoleAsync();
        }

        private async Task InitRoleAsync()
        {
            if (await _roleRepository.GetCountAsync() == 0)
            {
                var role = await _roleRepository.InsertAsync(new Role
                {
                    Name = "超级管理员",
                    Description = "权限最大,拥有所有权限"
                });

                var manager = await _managerRepository.InsertAsync(new Manager(Guid.NewGuid(), "admin", "123456", ""));

                await _managerRoleRepository.InsertAsync(ManagerRole.CreateManagerRole(manager, role));
            }
        }

        private async Task InitMenusAsync()
        {
            var menuList = MenuConfigSingle.CreatInstance().GetMenu();

            var list = await _repository.GetListAsync();

            menuList.ForEach(async p =>
            {
                var parentMenu = new Menu();
                parentMenu = await UpdateDBAsync(p, list, Guid.Empty);

                if (p.Items.Any())
                {
                    foreach (var citem in p.Items)
                    {
                        await UpdateDBAsync(citem, list, parentMenu.Id);
                    }
                }
            });
        }

        /// <summary>
        /// 将数据同步到数据库中
        /// </summary>
        /// <param name="p"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private async Task<Menu> UpdateDBAsync(ApplicationMenuItem p, List<Menu> list, Guid parentId)
        {
            Menu item;
            if (list.Exists(x => x.Url == p.Url && x.Name == p.Name))
            {
                var t = list.First(x => x.Url == p.Url && x.Name == p.Name);
                t.Name = p.Name;
                t.Order = p.Order;
                t.Url = p.Url;
                t.Icon = p.Icon;
                t.ParentId = parentId;
                item = await _repository.UpdateAsync(t);
            }
            else
            {
                item = await _repository.InsertAsync(new Menu
                {
                    Name = p.Name,
                    Order = p.Order,
                    Url = p.Url,
                    Icon = p.Icon,
                    ParentId = parentId
                });
            }

            return item;
        }
    }
}
