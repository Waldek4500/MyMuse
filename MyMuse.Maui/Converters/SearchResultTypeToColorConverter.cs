using MyMuse.Shared.Enums;
using System.Globalization;

namespace MyMuse.Maui.Converters;

public class SearchResultTypeToColorConverter : IValueConverter
{
    public string AlphaHex { get; set; } = "26";

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // Select color based on result type
        string color = value is SearchResultType resultType
            ? resultType switch
            {
                SearchResultType.Album => "#0891B2",
                SearchResultType.Artist => "#DB2777",
                _ => "#A855F7"
            }
            : "#A855F7";

        // Add alpha channel to color if Background parameter was passed
        if (parameter is string p && p.Equals("Background", StringComparison.OrdinalIgnoreCase))
            color = color.Insert(1, AlphaHex);

        return Color.FromArgb(color);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
