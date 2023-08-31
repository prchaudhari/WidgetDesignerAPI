using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WidgetDesignerAPI.API.Migrations
{
    /// <inheritdoc />
    public partial class modifyforeignkeypage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageWidgetsDetails_Pages_WidgetId",
                table: "PageWidgetsDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_PageWidgetsDetails_Widgets_WidgetId",
                table: "PageWidgetsDetails",
                column: "WidgetId",
                principalTable: "Widgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageWidgetsDetails_Widgets_WidgetId",
                table: "PageWidgetsDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_PageWidgetsDetails_Pages_WidgetId",
                table: "PageWidgetsDetails",
                column: "WidgetId",
                principalTable: "Pages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
