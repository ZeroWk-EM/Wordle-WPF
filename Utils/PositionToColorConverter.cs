using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Wordle_Library.Enum;

namespace WordleWPF.Utils
{
    public class PositionToColorConverter : IValueConverter
    {
        #region Proprietà
        public Brush? OkColorBrush { get; set; }

        public Brush? WrongColorBrush { get; set; }


        public Brush? MissingColorBrush { get; set; }

        public Brush? DefaultColorBrush { get; set; }

        #endregion

        #region Metodi
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case Position.Ok:
                    return OkColorBrush;
                case Position.Wrong:
                    return WrongColorBrush;
                case Position.Missing:
                    return MissingColorBrush;
                default:
                    return DefaultColorBrush;
            }
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
