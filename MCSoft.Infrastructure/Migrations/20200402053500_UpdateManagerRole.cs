using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MCSoft.Infrastructure.Migrations
{
    public partial class UpdateManagerRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_rbac_ManagerRoles",
                table: "rbac_ManagerRoles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "rbac_ManagerRoles");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "rbac_ManagerRoles");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "rbac_ManagerRoles");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "rbac_ManagerRoles");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "rbac_ManagerRoles");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "rbac_ManagerRoles");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "rbac_ManagerRoles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "rbac_ManagerRoles");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "rbac_ManagerRoles");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "rbac_ManagerRoles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_rbac_ManagerRoles",
                table: "rbac_ManagerRoles",
                columns: new[] { "ManagerId", "RoleId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_rbac_ManagerRoles",
                table: "rbac_ManagerRoles");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "rbac_ManagerRoles",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "rbac_ManagerRoles",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "rbac_ManagerRoles",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "rbac_ManagerRoles",
                type: "char(36)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "rbac_ManagerRoles",
                type: "char(36)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "rbac_ManagerRoles",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "rbac_ManagerRoles",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "rbac_ManagerRoles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "rbac_ManagerRoles",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "rbac_ManagerRoles",
                type: "char(36)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_rbac_ManagerRoles",
                table: "rbac_ManagerRoles",
                column: "Id");
        }
    }
}
