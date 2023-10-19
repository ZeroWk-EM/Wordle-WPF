using System;
using System.Windows;
using WordleWPF.ViewModels;

namespace WordleWPF.View
{

    public partial class WordleDialog : Window
    {
        #region Costruttori
        public WordleDialog(Action errorDialogAction, string? overrideMessage)
        {
            InitializeComponent();
            var vm = new DialogViewModel(errorDialogAction, overrideMessage);
            AcceptButton.Visibility = Visibility.Hidden;
            DenialButton.Content = "Chiudi app";
            vm.Title = "Errore";
            DataContext = vm;
        }

        public WordleDialog(Action<bool> hasWinner, string? WinnerWord, bool isWinner)
        {
            InitializeComponent();
            var vm = new DialogViewModel(hasWinner, WinnerWord!, isWinner);
            DataContext = vm;
            vm.Title = isWinner ? "Hai vinto" : "Hai Perso";
            vm.ButtonClicked += HandleButtonClicker;
        }
        #endregion

        #region Metodi
        private void HandleButtonClicker(bool isClicked)
        {
            if (isClicked)
            {
                this.Close();
            }
        }
        #endregion
    }
}
