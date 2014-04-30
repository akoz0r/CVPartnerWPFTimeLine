using System;
using System.Windows;
using System.Windows.Data;

namespace CVGuru.Converters
{
    public class ExistanceToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return Visibility.Collapsed;
            if(value is string && string.IsNullOrWhiteSpace(value as string))
                return Visibility.Collapsed;
            if (value.ToString() == "{NewItemPlaceholder}")
                return Visibility.Collapsed;
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
