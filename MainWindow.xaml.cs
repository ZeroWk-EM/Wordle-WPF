using NLog;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using WordleWPF.ViewModel;

namespace WordleWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        MainViewModel vm;
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
                MessageBox.Show("An internal error occured, please contact the technical support");
                _logger.Error($"{ex}");
                App.Current.Shutdown();
            }
        }

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
            AttemptInput.Text = "";
        }

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
    }
}
