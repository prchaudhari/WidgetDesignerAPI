using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WidgetDesignerAPI.API.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldWidget : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WidgetCSSUrl",
                table: "Widgets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WidgetCSSUrl",
                table: "Widgets");
        }
    }
}
