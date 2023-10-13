using NLog;
using System;
using System.Windows;
using System.Windows.Input;
using WordleWPF.ViewModel;

namespace WordleWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        public MainWindow()
        {
            InitializeComponent();
            var vm = new MainViewModel();
            DataContext = vm;
            try
            {
                vm.Init();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An internal error occured, please contact the technical support");
                _logger.Error($"{ex}");
                App.Current.Shutdown();
            }
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
