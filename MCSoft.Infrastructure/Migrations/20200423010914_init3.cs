using Microsoft.EntityFrameworkCore.Migrations;

namespace MCSoft.Infrastructure.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_app_products_app_heads_HeadId",
                table: "app_products");

            migrationBuilder.DropIndex(
                name: "IX_app_products_HeadId",
                table: "app_products");

            migrationBuilder.AddColumn<int>(
                name: "BrowseCount",
                table: "app_heads",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrowseCount",
                table: "app_heads");

            migrationBuilder.CreateIndex(
                name: "IX_app_products_HeadId",
                table: "app_products",
                column: "HeadId");

            migrationBuilder.AddForeignKey(
                name: "FK_app_products_app_heads_HeadId",
                table: "app_products",
                column: "HeadId",
                principalTable: "app_heads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
