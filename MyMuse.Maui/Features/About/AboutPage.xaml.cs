namespace MyMuse.Maui.Features.About;

public partial class AboutPage : ContentPage
{
	public AboutPage(AboutPageViewModel aboutPageViewModel)
	{
		BindingContext = aboutPageViewModel;
		InitializeComponent();
	}
}