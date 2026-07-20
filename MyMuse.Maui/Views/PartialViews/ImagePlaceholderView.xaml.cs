using Microsoft.Maui.Controls.Shapes;

namespace MyMuse.Maui.Views.PartialViews;

public partial class ImagePlaceholderView : ContentView
{
    public static readonly BindableProperty CornerRadiusProperty =
        BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(ImagePlaceholderView), default(CornerRadius));

    public static readonly BindableProperty ShowPlaceholderProperty =
       BindableProperty.Create(nameof(ShowPlaceholder), typeof(bool), typeof(ImagePlaceholderView), true);

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public bool ShowPlaceholder
    {
        get => (bool)GetValue(ShowPlaceholderProperty);
        set => SetValue(ShowPlaceholderProperty, value);
    }

    public ImagePlaceholderView()
	{
		InitializeComponent();
        BindingContext = this;
	}
}