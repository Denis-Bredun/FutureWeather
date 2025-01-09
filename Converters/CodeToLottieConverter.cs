using SkiaSharp.Extended.UI.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutureWeather.Converters
{
    public class CodeToLottieConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var code = (int)value;

            var lottieImageSource = new SKFileLottieImageSource();

            lottieImageSource.File = code switch
            {
                0 => "sunny.json",
                1 or 2 => "partly-cloudy.json",
                3 => "mist.json",
                45 or 48 => "foggy.json",
                51 or 53 or 55 => "partly-shower.json",
                56 or 57 => "partly-shower.json",
                61 or 63 or 65 => "stormshowersday.json",
                66 or 67 => "partly-shower.json",
                71 or 73 or 75 => "snow.json",
                77 => "snow-sunny.json",
                80 or 81 or 82 => "storm.json",
                85 or 86 => "snow-sunny.json",
                95 => "thunder.json",
                96 or 99 => "storm.json",
                _ => "windy.json"
            };

            return lottieImageSource;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
