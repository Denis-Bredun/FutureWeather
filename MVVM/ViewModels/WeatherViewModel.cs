using FutureWeather.MVVM.Models;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FutureWeather.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class WeatherViewModel
    {
        public WeatherData WeatherData { get; set; }
        public string PlaceName { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        private HttpClient _httpClient;

        public bool IsVisible { get; set; }
        public bool IsLoading { get; set; }

        public WeatherViewModel()
        {
            _httpClient = new HttpClient();
        }

        private async Task<Location> GetCoordinatesAsync(string address)
        {
            var locations = await Geocoding.Default.GetLocationsAsync(address);

            var location = locations?.FirstOrDefault();

            if (location != null)
                Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");

            return location;
        }

        public ICommand SearchCommand => new Command(async (searchText) =>
        {
            PlaceName = searchText.ToString();
            var location = await GetCoordinatesAsync(searchText.ToString());
            await GetWeather(location);
        });

        private async Task GetWeather(Location location)
        {
            var latitude = location.Latitude.ToString(CultureInfo.InvariantCulture);
            var longitude = location.Longitude.ToString(CultureInfo.InvariantCulture);

            var url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current=temperature_2m,relative_humidity_2m,apparent_temperature,is_day,precipitation,rain,showers,snowfall,weather_code,cloud_cover,pressure_msl,surface_pressure,wind_speed_10m,wind_direction_10m,wind_gusts_10m&daily=weather_code,temperature_2m_max,temperature_2m_min&timezone=Africa%2FCairo";

            IsLoading = true;

            var response = await _httpClient.GetAsync(url);

            if(response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<WeatherData>(responseStream);
                    WeatherData = data;

                    for (int i = 0; i < WeatherData.daily.time.Length; i++)
                    {
                        WeatherData.daily2.Add(new Daily2
                        {
                            time = WeatherData.daily.time[i],
                            temperature_2m_max = WeatherData.daily.temperature_2m_max[i],
                            temperature_2m_min = WeatherData.daily.temperature_2m_min[i],
                            weather_code = WeatherData.daily.weather_code[i]
                        });
                    }

                    IsVisible = true;
                }
            }
            IsLoading = false;
        }
    }
}
