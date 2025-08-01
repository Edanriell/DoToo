﻿using System.Globalization;

namespace DoToo.Converters;

internal class StatusColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (Color)Application.Current.Resources[(bool)value ? "CompletedColor" : "ActiveColor"];
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { return null; }
}