using MCSoft.Domain;
using MCSoft.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace MCSoft.Infrastructure.EntityFrameworkCore
{
    public static class MCSoftDbContextModelCreatingExtensions
    {
        public static void ConfigureDDDTest(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(DDDTestConsts.DbTablePrefix + "YourEntities", DDDTestConsts.DbSchema);

            //    //...
            //});

            #region RBAC

            builder.Entity<Manager>(b =>
            {
                b.ToTable(MCSoftConsts.DbTableRbacPrefix + "Managers", MCSoftConsts.DbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<Role>(b =>
            {
                b.ToTable(MCSoftConsts.DbTableRbacPrefix + "Roles", MCSoftConsts.DbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<Menu>(b =>
            {
                b.ToTable(MCSoftConsts.DbTableRbacPrefix + "Menus", MCSoftConsts.DbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<ManagerRole>(b =>
            {
                b.ToTable(MCSoftConsts.DbTableRbacPrefix + "ManagerRoles", MCSoftConsts.DbSchema);
                b.HasKey(x => new { x.ManagerId, x.RoleId });
                b.ConfigureByConvention();
            });

            builder.Entity<RoleMenu>(b =>
            {
                b.ToTable(MCSoftConsts.DbTableRbacPrefix + "RoleMenus", MCSoftConsts.DbSchema);
                b.HasKey(x => new { x.RoleId, x.MenuId });
                b.ConfigureByConvention();
            });

            #endregion

            #region App
            builder.Entity<User>(b =>
            {
                b.ToTable(MCSoftConsts.DbTablePrefix + "users", MCSoftConsts.DbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<Head>(b =>
            {
                b.ToTable(MCSoftConsts.DbTablePrefix + "heads", MCSoftConsts.DbSchema);
                b.OwnsOne(o => o.Address, a => {
                    a.WithOwner();
                });
                b.ConfigureByConvention();
            });

            builder.Entity<Category>(b =>
            {
                b.ToTable(MCSoftConsts.DbTablePrefix + "categorys", MCSoftConsts.DbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<Product>(b =>
            {
                b.ToTable(MCSoftConsts.DbTablePrefix + "products", MCSoftConsts.DbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<Cart>(b =>
            {
                b.ToTable(MCSoftConsts.DbTablePrefix + "carts", MCSoftConsts.DbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<Order>(b =>
            {
                b.ToTable(MCSoftConsts.DbTablePrefix + "orders", MCSoftConsts.DbSchema);
                b.OwnsOne(o => o.Address, a => {
                    a.WithOwner();
                });
                b.ConfigureByConvention();
            });

            builder.Entity<OrderItem>(b =>
            {
                b.ToTable(MCSoftConsts.DbTablePrefix + "orderitems", MCSoftConsts.DbSchema);
                b.HasKey(x => new { x.OrderId, x.ProductId });
                b.ConfigureByConvention();
            });
            #endregion
        }
    }
}
