namespace MyMuse.Maui.Features.Album;

public partial class AlbumPage : ContentPage
{
	public AlbumPage(AlbumPageViewModel albumPageViewModel)
	{
		InitializeComponent();
		BindingContext = albumPageViewModel;
	}
}