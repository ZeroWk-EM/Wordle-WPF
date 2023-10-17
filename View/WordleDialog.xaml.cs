using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WordleWPF.ViewModel;
using WordleWPF.ViewModels;

namespace WordleWPF.View
{

    public partial class WordleDialog : Window
    {
        public WordleDialog(Action<bool> IsWinner, string? WinnerWord, bool check)
        {
            InitializeComponent();
            var vm = new DialogViewModel(IsWinner, WinnerWord!, check);
            DataContext = vm;
        }


    }
}
