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

        public ContextDataBuilder(IRepository<Menu, Guid> repository)
        {
            _repository = repository;
        }

        public async void Build()
        {
            await InitMenusAsync();
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
