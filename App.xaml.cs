using FutureWeather.MVVM.Views;

namespace FutureWeather
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new WeatherView();
        }
    }
}
