using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMuse.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingImageColumnToGenres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AssociatedColor",
                table: "Genres",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Genres",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Genres");

            migrationBuilder.AlterColumn<string>(
                name: "AssociatedColor",
                table: "Genres",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
