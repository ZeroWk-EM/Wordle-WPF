using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Wordle_Library.Enum;

namespace WordleWPF.Utils
{
    public class PositionToColorConverter : IValueConverter
    {
        #region Proprietà
        public static Brush? OkColorBrush
        {
            get
            {
                return Application.Current.MainWindow.Resources["RightPosition"] as SolidColorBrush;
            }
            set
            {
                Application.Current.MainWindow.Resources["RightPosition"] = value;
            }
        }

        public static Brush? WrongColorBrush
        {
            get
            {
                return Application.Current.MainWindow.Resources["WrongPosition"] as SolidColorBrush;
            }
            set
            {
                Application.Current.MainWindow.Resources["WrongPosition"] = value;
            }
        }

        public static Brush? MissingColorBrush
        {
            get
            {
                return Application.Current.MainWindow.Resources["MissingPosition"] as SolidColorBrush;
            }
            set
            {
                Application.Current.MainWindow.Resources["MissingPosition"] = value;
            }
        }
        public static Brush? DefaultColorBrush
        {
            get
            {
                return Application.Current.MainWindow.Resources["DefaultCellColor"] as SolidColorBrush;
            }
            set
            {
                Application.Current.MainWindow.Resources["DefaultCellColor"] = value;
            }
        }
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
