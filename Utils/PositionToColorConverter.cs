using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Wordle_Library.Enum;

namespace WordleWPF.Utils
{
    public class PositionToColorConverter : IValueConverter
    {
        #region Metodi
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case Position.Ok:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#80FF72"));
                case Position.Wrong:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFEE70"));
                case Position.Missing:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E66760"));
                default:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CCFFFFFF"));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
