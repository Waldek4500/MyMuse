using MyMuse.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMuse.Shared.Dtos
{
    public record SearchResultDto
    (
        int Id,
        SearchResultType Type,
        string Title,
        string? Subtitle,
        string? ImageUrl
    );
}
