using System;
using System.Globalization;
using Xamarin.Forms;

namespace Eggsclaim.ValueConverters
{
    public class TimestampConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
            {
                var timestamp = (DateTime)value;
                return timestamp.ToLocalTime().ToString("hh:mm tt dddd, MMMM d");
            }
            return value;
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
