using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WordleWPF.ViewModels
{
    public class DialogViewModel : INotifyPropertyChanged
    {
        #region Variabili
        public event PropertyChangedEventHandler? PropertyChanged;
        public event Action<bool> ButtonClicked;

        private readonly Action<bool> _hasWinner;
        private readonly Action _closeApp;

        private string _title;
        private string _paragraph = "";


        public DelegateCommand YesButtonCommand { get; }
        public DelegateCommand NoButtonCommand { get; }
        #endregion

        #region Costruttori
        public DialogViewModel(Action errorDialogAction, string? ovveraidMessage)
        {
            _closeApp = errorDialogAction;
            Paragraph = ovveraidMessage != null ? ovveraidMessage : "Non è stato possibile avviare il gioco a causa di un errore imprevisto, contattare il supporto tecnico.";
            NoButtonCommand = new(CloseApp);
        }

        public DialogViewModel(Action<bool> hasWinner, string WinnerWord, bool isWinner)
        {
            _hasWinner = hasWinner;
            Paragraph = $"{"Hai " + (isWinner ? "vinto" : "perso")}" + $"{(isWinner ? "" : (" la parola corretta è " + $"{WinnerWord.ToUpper()}"))}" + $"{", vuoi giorcare ancora?"}";
            YesButtonCommand = new(YesButton);
            NoButtonCommand = new(NoButton);
        }
        #endregion

        #region Proprietà
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (_title != value)
                {
                    _title = value;
                }
            }
        }

        public string Paragraph
        {
            get
            {
                return _paragraph;
            }
            set
            {
                if (_paragraph != value)
                {
                    _paragraph = value;
                }
            }
        }
        #endregion

        #region Metodi
        private void YesButton(object? o)
        {
            _hasWinner(true);
            // Invio una notifica al model per far capire chiamarare
            // la funzione che chiude in automatico la modal
            ButtonClicked.Invoke(true);
        }

        private void NoButton(object? o)
        {
            _hasWinner(false);
            ButtonClicked.Invoke(true);
        }

        private void CloseApp(object? o)
        {
            _closeApp();
        }
        #endregion

        #region Eventi
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
