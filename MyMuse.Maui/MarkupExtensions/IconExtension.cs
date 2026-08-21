namespace MyMuse.Maui.MarkupExtensions;

[ContentProperty(nameof(Glyph))]
public class IconExtension : IMarkupExtension<ImageSource>
{
    public string? Glyph { get; set; }

    public Color Color { get; set; } = Colors.White;
    public double Size { get; set; } = 24;

    public ImageSource ProvideValue(IServiceProvider serviceProvider)
    {
        return new FontImageSource
        {
            Glyph = this.Glyph,
            FontFamily = "MaterialSymbols",
            Color = this.Color,
            Size = this.Size
        };
    }

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        => ProvideValue(serviceProvider);
}
