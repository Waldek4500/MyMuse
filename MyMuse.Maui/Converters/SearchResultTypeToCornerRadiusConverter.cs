using MyMuse.Shared.Enums;
using System.Globalization;

namespace MyMuse.Maui.Converters;

public class SearchResultTypeToCornerRadiusConverter : IValueConverter
{
    public CornerRadius DefaultValue { get; set; } = 15;
    public CornerRadius ArtistsValue { get; set; } = 40;

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is SearchResultType resultType && resultType == SearchResultType.Artist)
            return ArtistsValue;

        return DefaultValue;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
