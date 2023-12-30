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
        private HashSet<char> _wrongPositionChar;
        private HashSet<char> _missingPositionChar;
        #endregion

        #region Costruttore
        public AttemptViewModel(int wordLength)
        {
            _characters = new ObservableCollection<CharacterViewModel>();
            _wrongPositionChar = new();
            _missingPositionChar = new();
            CreateList(wordLength);
        }
        #endregion

        #region Proprietà
        public IEnumerable<CharacterViewModel> Characters
        {
            get { return _characters; }
        }

        public HashSet<char> WrongPositionChar
        {
            get
            {
                return _wrongPositionChar;
            }
        }

        public HashSet<char> MissingPositionChar
        {
            get
            {
                return _missingPositionChar;
            }
        }
        #endregion

        #region Metodi
        private void CreateList(int wordLength)
        {
            for (int i = 0; i < wordLength; i++)
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
                else if (WrongPositionChar.Contains(attempt.Value) && attempt.CellPosition == Position.Ok)
                {
                    _wrongPositionChar.Remove(attempt.Value);
                    OnPropertyChanged(nameof(WrongPositionChar));
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
