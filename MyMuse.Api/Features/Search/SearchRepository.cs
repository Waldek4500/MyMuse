using Microsoft.EntityFrameworkCore;
using MyMuse.Api.Data;
using MyMuse.Api.Entities;

namespace MyMuse.Api.Features.Search
{
    public class SearchRepository(MyMuseContext dbContext) : ISearchRepository
    {
        private readonly MyMuseContext _dbContext = dbContext;

        public async Task<List<SearchResultItem>> SearchAsync(SearchQuery query, CancellationToken ct)
        {
            var sql = @"
                SET pg_trgm.similarity_threshold = 0.1;

                SELECT * FROM (
                    SELECT s.""Id"",
                            'song' AS ""Type"",
                            s.""Title"" AS ""Title"",
                            ar.""Name"" AS ""Subtitle"",
                            COALESCE(s.""ImageUrl"", al.""ImageUrl"") AS ""ImageUrl"",
                            similarity(s.""Title"", {0}) AS ""Score""
                    FROM ""Songs"" s
                    JOIN ""Artists"" ar ON ar.""Id"" = s.""ArtistId""
                    LEFT JOIN ""Albums"" al ON al.""Id"" = s.""AlbumId""
                    WHERE s.""Title"" % {0} OR similarity(ar.""Name"", {0}) > 0.5

                    UNION ALL

                    SELECT al.""Id"",
                            'album',
                            al.""Title"",
                            ar.""Name"",
                            al.""ImageUrl"",
                            similarity(al.""Title"", {0})
                    FROM ""Albums"" al
                    JOIN ""Artists"" ar ON ar.""Id"" = al.""ArtistId""
                    WHERE al.""Title"" % {0} OR similarity(ar.""Name"", {0}) > 0.5

                    UNION ALL

                    SELECT ar.""Id"",
                            'artist',
                            ar.""Name"",
                            NULL,
                            ar.""ImageUrl"",
                            similarity(ar.""Name"", {0})
                    FROM ""Artists"" ar
                    WHERE ar.""Name"" % {0}
                ) combined
                ORDER BY ""Score"" DESC, ""Id""
                OFFSET {1}
                LIMIT {2};";

            int skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await _dbContext.SearchResults
                .FromSqlRaw(sql, query.SearchFor, skipNumber, query.PageSize)
                .ToListAsync(ct);
        }
    }
}
