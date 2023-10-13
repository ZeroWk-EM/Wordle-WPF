using System;
using System.Windows;
using System.Windows.Input;
using WordleWPF.ViewModel;
using WordleWPF.ViewModels;

namespace WordleWPF.View
{

    public partial class WordleDialog : Window
    {
        public WordleDialog()
        {
            InitializeComponent();
            if (Application.Current.MainWindow != null)
            {
                Application.Current.MainWindow.IsEnabled = false;
            }
            var vm = new  DialogViewModel();
            DataContext = vm;
         }

        private void ChildWindow_Closed(object sender, EventArgs e)
        {
            if (Application.Current.MainWindow != null)
            {
                Application.Current.MainWindow.IsEnabled = true;
            }
        }
    }
}
