using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Wordle_Library.Enum;

namespace WordleWPF.ViewModel
{
    public class AttemptViewModel : INotifyPropertyChanged
    {
        #region Variabili
        public event PropertyChangedEventHandler? PropertyChanged;
        private ObservableCollection<CharacterViewModel> _characters;
        private List<char> _wrongPositionChar;
        private List<char> _missingPositionChar;
        #endregion

        #region Costruttore
        public AttemptViewModel(string word)
        {
            _characters = new ObservableCollection<CharacterViewModel>();
            _wrongPositionChar = new();
            _missingPositionChar = new();
            CreateList(word);
        }
        #endregion

        #region Proprietà
        public IEnumerable<CharacterViewModel> Characters
        {
            get { return _characters; }
        }

        public List<char> WrongPositionChar
        {
            get
            {
                return new List<char>(_wrongPositionChar);
            }
        }

        public List<char> MissingPositionChar
        {
            get
            {
                return new List<char>(_missingPositionChar);
            }
        }
        #endregion

        #region Metodi
        private void CreateList(string word)
        {
            for (int i = 0; i < word.Length; i++)
            {
                _characters.Add(new CharacterViewModel());
            }
        }

        public void SetViewCharacter(string word, List<Position> rowAttempt)
        {
            int i = 0;
            foreach (var attempt in Characters)
            {
                attempt.Value = word[i];
                attempt.CellPosition = rowAttempt[i];
                if (attempt.CellPosition == Position.Wrong)
                {
                    _wrongPositionChar.Add(attempt.Value);
                    OnPropertyChanged(nameof(WrongPositionChar));
                }
                else if (attempt.CellPosition == Position.Missing)
                {
                    _missingPositionChar.Add(attempt.Value);
                    OnPropertyChanged(nameof(MissingPositionChar));
                }
                i++;
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
