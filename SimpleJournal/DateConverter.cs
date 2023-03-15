using System.Globalization;
using System.Windows.Data;
using System.Windows;
using System;

namespace SimpleJournal;

[ValueConversion(typeof(DateOnly), typeof(string))]
public class DateConverter : IValueConverter
{
    private static readonly CultureInfo Ru = new("ru-RU");
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var date = (DateOnly)value;
        return date.ToString(Ru);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var strValue = (string) value;
        if (DateOnly.TryParse(
                strValue, 
                Ru, 
                DateTimeStyles.AllowWhiteSpaces, 
                out DateOnly resultDateTime))
        {
            return resultDateTime;
        }
        
        return DependencyProperty.UnsetValue;
    }
}