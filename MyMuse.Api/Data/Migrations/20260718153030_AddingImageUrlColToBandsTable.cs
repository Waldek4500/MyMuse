using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMuse.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingImageUrlColToBandsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Bands",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Bands");
        }
    }
}
