using System;
using System.Globalization;
using Xamarin.Forms;


namespace WeatherApp
{
    public class RoundAndUnitConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           return Math.Round((double)value).ToString() + "\x00B0" + "C";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
