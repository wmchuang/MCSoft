using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.UI.Navigation;

namespace MCSoft.Infrastructure
{
    public sealed class MenuConfigSingle
    {
        private static MenuConfigSingle _configSingle = new MenuConfigSingle();

        private MenuConfigSingle() { }

        public static MenuConfigSingle CreatInstance()
        {
            return _configSingle;
        }

        public ApplicationMenuItemList GetMenu()
        {
            var menuList = new ApplicationMenuItemList();
            var t = "&_&";

            var item = new ApplicationMenuItem("后台管理", t, "#", icon: "&#xe726;", order: 0);
            item.Items.Add(new ApplicationMenuItem("管理员管理", t, "/Manager"));
            item.Items.Add(new ApplicationMenuItem("角色管理", t, "/Role"));
            menuList.Add(item);

            item = new ApplicationMenuItem("商品管理", t, "#", icon: "&#xe6f6;", order: 0);
            item.Items.Add(new ApplicationMenuItem("分类管理", t, "/Category"));
            item.Items.Add(new ApplicationMenuItem("商品列表", t, "/Product"));
            menuList.Add(item);

            menuList.Normalize();
            return menuList;
        }

    }
}
