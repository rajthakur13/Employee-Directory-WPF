using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EmployeeDirectory
{
    public class NullToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool invert = parameter != null && parameter.ToString() == "True";

            if (value == null)
            {
                return invert ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            }

            return invert ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
