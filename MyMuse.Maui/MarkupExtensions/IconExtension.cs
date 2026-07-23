using MyMuse.Maui.Resources.Images.Icons;

namespace MyMuse.Maui.MarkupExtensions
{
    [ContentProperty(nameof(Name))]
    public class IconExtension : IMarkupExtension<ImageSource>
    {
        public string? Name { get; set; }

        public Color Color { get; set; } = Colors.White;
        public double Size { get; set; } = 24;

        public ImageSource ProvideValue(IServiceProvider serviceProvider)
        {
            var glyph = Name switch
            {
                "Home" => MaterialIcons.Home,
                "Search" => MaterialIcons.Search,
                "Browse" => MaterialIcons.Browse,
                "Favorite" => MaterialIcons.Favorite,
                "ArrowDown" => MaterialIcons.ArrowDown,
                "MoreHorizontal" => MaterialIcons.MoreHorizontal,
                "AlignLeft" => MaterialIcons.AlignLeft,
                "SkipPrevious" => MaterialIcons.SkipPrevious,
                "SkipNext" => MaterialIcons.SkipNext,
                "Autorenew" => MaterialIcons.Autorenew,
                _ => throw new ArgumentException($"Unknown icon: {Name}")
            };

            return new FontImageSource
            {
                Glyph = glyph,
                FontFamily = "MaterialSymbols",
                Color = this.Color,
                Size = this.Size
            };
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
            => ProvideValue(serviceProvider);
    }
}
