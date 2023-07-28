using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WidgetDesignerAPI.API.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsWidgetCss_Widget1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WidgetCSS",
                table: "Widgets",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WidgetCSS",
                table: "Widgets");
        }
    }
}
