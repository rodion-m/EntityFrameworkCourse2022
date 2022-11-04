using System.Globalization;
using System.Windows.Data;
using System.Windows;
using System;

namespace SimpleJournal;

[ValueConversion(typeof(DateOnly), typeof(string))]
public class DateConverter : IValueConverter
{
    private static readonly CultureInfo s_ru = new("ru-RU");
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var date = (DateOnly)value;
        return date.ToString(s_ru);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var strValue = (string) value;
        if (DateOnly.TryParse(
                strValue, 
                s_ru, 
                DateTimeStyles.AllowWhiteSpaces, 
                out DateOnly resultDateTime))
        {
            return resultDateTime;
        }
        
        return DependencyProperty.UnsetValue;
    }
}