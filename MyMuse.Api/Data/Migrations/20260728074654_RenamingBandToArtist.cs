using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyMuse.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenamingBandToArtist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop FKs referencing Bands (names will change since they reference the old table name)
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Bands_BandId",
                table: "Albums");

            migrationBuilder.DropForeignKey(
                name: "FK_BandMembers_Bands_BandId",
                table: "BandMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Bands_BandId",
                table: "Songs");

            // Rename the table itself (keeps all data, just renames)
            migrationBuilder.RenameTable(
                name: "Bands",
                newName: "Artists");

            // Rename the primary key constraint to match the new table name
            migrationBuilder.Sql(
                "ALTER TABLE \"Artists\" RENAME CONSTRAINT \"PK_Bands\" TO \"PK_Artists\";");

            // Rename FK columns
            migrationBuilder.RenameColumn(
                name: "BandId",
                table: "Songs",
                newName: "ArtistId");

            migrationBuilder.RenameIndex(
                name: "IX_Songs_BandId",
                table: "Songs",
                newName: "IX_Songs_ArtistId");

            migrationBuilder.RenameColumn(
                name: "BandId",
                table: "BandMembers",
                newName: "ArtistId");

            migrationBuilder.RenameIndex(
                name: "IX_BandMembers_BandId",
                table: "BandMembers",
                newName: "IX_BandMembers_ArtistId");

            migrationBuilder.RenameColumn(
                name: "BandId",
                table: "Albums",
                newName: "ArtistId");

            migrationBuilder.RenameIndex(
                name: "IX_Albums_BandId",
                table: "Albums",
                newName: "IX_Albums_ArtistId");

            // Recreate FKs pointing at the renamed Artists table
            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Artists_ArtistId",
                table: "Albums",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BandMembers_Artists_ArtistId",
                table: "BandMembers",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Artists_ArtistId",
                table: "Songs",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Artists_ArtistId",
                table: "Albums");

            migrationBuilder.DropForeignKey(
                name: "FK_BandMembers_Artists_ArtistId",
                table: "BandMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Artists_ArtistId",
                table: "Songs");

            migrationBuilder.RenameColumn(
                name: "ArtistId",
                table: "Songs",
                newName: "BandId");

            migrationBuilder.RenameIndex(
                name: "IX_Songs_ArtistId",
                table: "Songs",
                newName: "IX_Songs_BandId");

            migrationBuilder.RenameColumn(
                name: "ArtistId",
                table: "BandMembers",
                newName: "BandId");

            migrationBuilder.RenameIndex(
                name: "IX_BandMembers_ArtistId",
                table: "BandMembers",
                newName: "IX_BandMembers_BandId");

            migrationBuilder.RenameColumn(
                name: "ArtistId",
                table: "Albums",
                newName: "BandId");

            migrationBuilder.RenameIndex(
                name: "IX_Albums_ArtistId",
                table: "Albums",
                newName: "IX_Albums_BandId");

            // Rename PK constraint back
            migrationBuilder.Sql(
                "ALTER TABLE \"Artists\" RENAME CONSTRAINT \"PK_Artists\" TO \"PK_Bands\";");

            // Rename table back
            migrationBuilder.RenameTable(
                name: "Artists",
                newName: "Bands");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Bands_BandId",
                table: "Albums",
                column: "BandId",
                principalTable: "Bands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BandMembers_Bands_BandId",
                table: "BandMembers",
                column: "BandId",
                principalTable: "Bands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Bands_BandId",
                table: "Songs",
                column: "BandId",
                principalTable: "Bands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}