using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMuse.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingSearchExtension : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:pg_trgm", ",,");

            migrationBuilder.Sql("CREATE INDEX idx_songs_title_trgm ON \"Songs\" USING gin (\"Title\" gin_trgm_ops);");
            migrationBuilder.Sql("CREATE INDEX idx_albums_title_trgm ON \"Albums\" USING gin (\"Title\" gin_trgm_ops);");
            migrationBuilder.Sql("CREATE INDEX idx_artists_name_trgm ON \"Artists\" USING gin (\"Name\" gin_trgm_ops);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:pg_trgm", ",,");

            migrationBuilder.Sql("DROP INDEX IF EXISTS idx_songs_title_trgm;");
            migrationBuilder.Sql("DROP INDEX IF EXISTS idx_albums_title_trgm;");
            migrationBuilder.Sql("DROP INDEX IF EXISTS idx_artists_name_trgm;");
        }
    }
}
