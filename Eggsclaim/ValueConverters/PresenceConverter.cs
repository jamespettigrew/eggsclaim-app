using System;
using System.Globalization;
using Xamarin.Forms;

namespace Eggsclaim.ValueConverters
{
    public class PresenceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                var eggsPresent = (bool)value;
                return (eggsPresent) ? "Eggs waiting!" : "Collected!";
            }
            return value;
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
