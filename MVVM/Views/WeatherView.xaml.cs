using FutureWeather.MVVM.ViewModels;

namespace FutureWeather.MVVM.Views;

public partial class WeatherView : ContentPage
{
	public WeatherView()
	{
		InitializeComponent();

		BindingContext = new WeatherViewModel();
	}
}