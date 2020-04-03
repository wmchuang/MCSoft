using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MCSoft.Infrastructure.Migrations
{
    public partial class UpdateRoleMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RbacRoleMenus",
                table: "RbacRoleMenus");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RbacRoleMenus");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "RbacRoleMenus");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "RbacRoleMenus");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "RbacRoleMenus");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "RbacRoleMenus");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "RbacRoleMenus");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "RbacRoleMenus");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RbacRoleMenus");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "RbacRoleMenus");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "RbacRoleMenus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RbacRoleMenus",
                table: "RbacRoleMenus",
                columns: new[] { "RoleId", "MenuId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RbacRoleMenus",
                table: "RbacRoleMenus");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "RbacRoleMenus",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "RbacRoleMenus",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "RbacRoleMenus",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "RbacRoleMenus",
                type: "char(36)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "RbacRoleMenus",
                type: "char(36)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "RbacRoleMenus",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "RbacRoleMenus",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RbacRoleMenus",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "RbacRoleMenus",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "RbacRoleMenus",
                type: "char(36)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RbacRoleMenus",
                table: "RbacRoleMenus",
                column: "Id");
        }
    }
}
