using System;
using System.Globalization;
using System.Windows.Data;

namespace EFSM.Designer.Converters
{
    public class CenterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double)
            {
                return ((double)value) / 2 * -1;
            }

            if (value is float)
            {
                return ((float)value) / 2 * -1;
            }

            if (value is int)
            {
                return ((int)value) / 2 * -1;
            }

            if (value is short)
            {
                return ((short)value) / 2 * -1;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
