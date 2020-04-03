using MCSoft.Application.Service;
using MCSoft.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace MCSoft.Components
{
    [ViewComponent(Name = "Navigation")]
    public class NavigationViewComponent : ViewComponent
    {
        private readonly ManagerAppService _managerAppService;
        public NavigationViewComponent(ManagerAppService managerAppService)
        {
            _managerAppService = managerAppService;
        }
        public IViewComponentResult Invoke()
        {
            //var naviagtion = MenuConfigSingle.CreatInstance().GetMenu();

            var list = new ApplicationMenuItemList();

            var currentUserMenu = _managerAppService.GetCurrentManagerMenuAsync().Result;
            foreach (var item in currentUserMenu)
            {
                if (item.ParentId == Guid.Empty)
                {
                    list.Add(new ApplicationMenuItem(item.Name, item.Id.ToString(), item.Url, item.Icon, item.Order));
                }
                else
                {
                    var m = list.FirstOrDefault(x => x.DisplayName == item.ParentId.ToString() && x.Url == "#").Items;
                    m.Add(new ApplicationMenuItem(item.Name, item.Id.ToString(), item.Url, item.Icon, item.Order));
                }
            }

            return View(list);
        }
    }
}
