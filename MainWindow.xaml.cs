using System.Windows;
using WordleWPF.ViewModel;
using System.Windows.Threading;
using System;
using NLog;

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
                MessageBox.Show("Cristo");
                _logger.Error($"{ex}");
                App.Current.Shutdown();
            }
        }
    }
}
