using Microsoft.EntityFrameworkCore.Migrations;

namespace MCSoft.Infrastructure.Migrations
{
    public partial class UpdateTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RbacRoles",
                table: "RbacRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RbacRoleMenus",
                table: "RbacRoleMenus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RbacMenus",
                table: "RbacMenus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RbacManagers",
                table: "RbacManagers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RbacManagerRoles",
                table: "RbacManagerRoles");

            migrationBuilder.RenameTable(
                name: "RbacRoles",
                newName: "rbac_Roles");

            migrationBuilder.RenameTable(
                name: "RbacRoleMenus",
                newName: "rbac_RoleMenus");

            migrationBuilder.RenameTable(
                name: "RbacMenus",
                newName: "rbac_Menus");

            migrationBuilder.RenameTable(
                name: "RbacManagers",
                newName: "rbac_Managers");

            migrationBuilder.RenameTable(
                name: "RbacManagerRoles",
                newName: "rbac_ManagerRoles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_rbac_Roles",
                table: "rbac_Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_rbac_RoleMenus",
                table: "rbac_RoleMenus",
                columns: new[] { "RoleId", "MenuId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_rbac_Menus",
                table: "rbac_Menus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_rbac_Managers",
                table: "rbac_Managers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_rbac_ManagerRoles",
                table: "rbac_ManagerRoles",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_rbac_Roles",
                table: "rbac_Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_rbac_RoleMenus",
                table: "rbac_RoleMenus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_rbac_Menus",
                table: "rbac_Menus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_rbac_Managers",
                table: "rbac_Managers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_rbac_ManagerRoles",
                table: "rbac_ManagerRoles");

            migrationBuilder.RenameTable(
                name: "rbac_Roles",
                newName: "RbacRoles");

            migrationBuilder.RenameTable(
                name: "rbac_RoleMenus",
                newName: "RbacRoleMenus");

            migrationBuilder.RenameTable(
                name: "rbac_Menus",
                newName: "RbacMenus");

            migrationBuilder.RenameTable(
                name: "rbac_Managers",
                newName: "RbacManagers");

            migrationBuilder.RenameTable(
                name: "rbac_ManagerRoles",
                newName: "RbacManagerRoles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RbacRoles",
                table: "RbacRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RbacRoleMenus",
                table: "RbacRoleMenus",
                columns: new[] { "RoleId", "MenuId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RbacMenus",
                table: "RbacMenus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RbacManagers",
                table: "RbacManagers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RbacManagerRoles",
                table: "RbacManagerRoles",
                column: "Id");
        }
    }
}
