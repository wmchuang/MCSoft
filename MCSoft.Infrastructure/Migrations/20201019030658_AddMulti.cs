using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MCSoft.Infrastructure.Migrations
{
    public partial class AddMulti : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "app_handlelogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "app_handlelogs");
        }
    }
}
