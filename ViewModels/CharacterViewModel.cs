using System.ComponentModel;
using System.Runtime.CompilerServices;
using Wordle_Library.Enum;

namespace WordleWPF.ViewModel
{
    public class CharacterViewModel : INotifyPropertyChanged
    {
        #region Variabili
        public event PropertyChangedEventHandler? PropertyChanged;
        private char _value = ' ';
        private Position? _position;
        #endregion

        #region Costruttore
        public CharacterViewModel()
        {

        }
        #endregion

        #region Proprietà
        public char Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

        public Position? CellPosition
        {
            get { return _position; }
            set
            {
                if (value != _position)
                {
                    _position = value;
                    OnPropertyChanged();
                }
            }
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
