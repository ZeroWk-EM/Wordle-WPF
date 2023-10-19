using NLog;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using WordleWPF.View;
using WordleWPF.ViewModel;

namespace WordleWPF
{
    public partial class MainWindow : Window
    {
        #region Variabili
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        MainViewModel vm;
        #endregion

        #region Costruttore
        public MainWindow()
        {
            InitializeComponent();
            vm = new MainViewModel();
            DataContext = vm;
            try
            {
                vm.Init();
                vm.BlinkTextBox += BlinkEvent;
            }
            catch (Exception ex)
            {
                WordleDialog errorDialog = new(() => { Application.Current.Shutdown(); }, null);
                errorDialog.ShowDialog();
                _logger.Error($"{ex}");
            }
        }
        #endregion

        #region Metodi
        private async void BlinkEvent()
        {
            SolidColorBrush? originalColorBrush = AttemptBox.Background as SolidColorBrush;
            SolidColorBrush? errorColorBrush = Application.Current.MainWindow.Resources["MissingPosition"] as SolidColorBrush;

            for (int i = 0; i < 2; i++)
            {
                AttemptBox.Background = errorColorBrush;
                await Task.Delay(100);
                AttemptBox.Background = originalColorBrush;
                await Task.Delay(100);
            }
            AttemptBox.Background = originalColorBrush;
        }
        #endregion

        #region Eventi
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (DataContext as MainViewModel)?.VerifyWordCommand?.Execute(null);
            }
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
        #endregion
    }
}
