using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutureWeather.Converters
{
    public class CodeToWeatherConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var code = (int)value;

            return code switch
            {
                0 => "Clear sky",
                1 or 2 or 3 => "Partly cloudy",
                45 or 48 => "Foggy",
                51 or 53 or 55 => "Light drizzle",
                56 or 57 => "Freezing drizzle",
                61 or 63 or 65 => "Rainy",
                66 or 67 => "Freezing rain",
                71 or 73 or 75 => "Snowfall",
                77 => "Snow grains",
                80 or 81 or 82 => "Rain showers",
                85 or 86 => "Snow showers",
                95 => "Thunderstorm",
                96 or 99 => "Hailstorm",
                _ => "Unknown"
            };
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
