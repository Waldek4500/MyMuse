using System;
using System.Collections.Generic;
using System.Text;

namespace MyMuse.Shared.Dtos
{
    public record BandDto(int Id, string Name, DateOnly FoundingDate, string? Description);
}
