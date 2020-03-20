using System;
using System.Globalization;
using Xamarin.Forms;

namespace WeatherApp
{
    public class CapitalizeFirstConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string original))
                return null;

            if (original.Length == 0)
                return string.Empty;

            var upperChar = char.ToUpper(original[0]);
            var withoutFirst = original.Remove(0, 1);

            return withoutFirst.Insert(0, upperChar.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotSupportedException();
    }
}
