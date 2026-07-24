using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMuse.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class SettingDefaultValueForGenresColor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AssociatedColor",
                table: "Genres",
                type: "text",
                nullable: false,
                defaultValue: "#7C3AED",
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AssociatedColor",
                table: "Genres",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "#7C3AED");
        }
    }
}
