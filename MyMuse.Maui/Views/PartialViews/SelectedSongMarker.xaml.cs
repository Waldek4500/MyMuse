namespace MyMuse.Maui.Views.PartialViews;

public partial class SelectedSongMarker : ContentView
{
	public SelectedSongMarker()
	{
		InitializeComponent();
	}

	public static readonly BindableProperty CornerRadiusProperty =
		BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(SelectedSongMarker), new CornerRadius(20));

    public static readonly BindableProperty PlayingImageHeightProperty =
        BindableProperty.Create(nameof(PlayingImageHeight), typeof(double), typeof(SelectedSongMarker), 40d);

    public static readonly BindableProperty PauseImageHeightProperty =
        BindableProperty.Create(nameof(PauseImageHeight), typeof(double), typeof(SelectedSongMarker), 60d);

    public CornerRadius CornerRadius
	{
		get => (CornerRadius)GetValue(CornerRadiusProperty);
		set => SetValue(CornerRadiusProperty, value);
	}

	public double PlayingImageHeight
	{
		get => (double)GetValue(PlayingImageHeightProperty);
		set => SetValue(PlayingImageHeightProperty, value);
	}

    public double PauseImageHeight
    {
        get => (double)GetValue(PauseImageHeightProperty);
        set => SetValue(PauseImageHeightProperty, value);
    }
}