using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMuse.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingColorColumnInGenres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssociatedColor",
                table: "Genres",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssociatedColor",
                table: "Genres");
        }
    }
}
