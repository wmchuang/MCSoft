using MCSoft.Domain;
using MCSoft.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace MCSoft.Infrastructure.EntityFrameworkCore
{
    [ConnectionStringName("Default")]
    public class MCSoftDbContext : AbpDbContext<MCSoftDbContext>
    {
        public DbSet<Manager> Managers { get; set; }

        public DbSet<ManagerRole> ManagerRoles { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<RoleMenu> RoleMenus { get; set; }

        #region APP
        public DbSet<User> Users { get; set; }

        public DbSet<Head> Heads { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Evaluate> Evaluates { get; set; }

        #endregion

        public MCSoftDbContext(DbContextOptions<MCSoftDbContext> options)
           : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            //base.OnModelCreating(builder);
            /* Configure the shared tables (with included modules) here */

            //builder.Entity<Test>(b =>
            //{
            //    b.ToTable("AbpUsers"); //Sharing the same table "AbpUsers" with the IdentityUser
            //    b.ConfigureByConvention();
            //    b.ConfigureAbpUser();

            //    //Moved customization to a method so we can share it with the DDDTestMigrationsDbContext class
            //    b.ConfigureCustomUserProperties();
            //});

            /* Configure your own tables/entities inside the ConfigureDDDTest method */

            builder.ConfigureDDDTest();

            base.OnModelCreating(builder);
        }
    }
}
