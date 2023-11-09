using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Timers;
using System.Windows;
using Wordle_Library;
using Wordle_Library.Enum;
using WordleWPF.View;
using WordleWPF.ViewModels;

namespace WordleWPF.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Variabili
        Logic? gm;
        public event PropertyChangedEventHandler? PropertyChanged;
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private int _currentAttempt = 0;
        private const int _maxAttempt = 6;
        private string? _path;
        private string? _winnerWord;
        private List<AttemptViewModel>? _attempts;
        private HashSet<char> _wrongPositionChar;
        private HashSet<char> _missingPositionChar;
        private string _wordAttempt = "";
        private readonly Stopwatch _stopwatch;
        private readonly Timer _interval;
        private string _time = "00:00:00";
        private int _point = 0;
        private int _wordLen;
        private HashSet<string> _usedWords;
        public Action BlinkTextBox;

        public DelegateCommand RestartGameCommand { get; }
        public DelegateCommand VerifyWordCommand { get; }
        #endregion

        #region Costruttore
        public MainViewModel()
        {
            RestartGameCommand = new DelegateCommand(RestartGame);
            VerifyWordCommand = new DelegateCommand(VerifyWord, CanPressed);
            _wordLen = 0;
            _wrongPositionChar = new();
            _missingPositionChar = new();
            _usedWords = new();
            _stopwatch = new Stopwatch();
            _interval = new Timer(1000);
            _interval.Elapsed += TimerElapsed;
            _stopwatch.Start();
            _interval.Start();
        }
        #endregion

        #region Proprietà
        public IEnumerable<AttemptViewModel> Attempts { get { return _attempts!; } }

        public string? WinnerWord
        {
            get
            {
                return _winnerWord;
            }
            set { _winnerWord = value; }
        }

        public int WordLen
        {
            get
            {
                return _wordLen;
            }
            private set
            {
                _wordLen = _winnerWord.Length;
                OnPropertyChanged();
            }

        }

        public string WordAttempt
        {
            get
            {
                return _wordAttempt;
            }
            set
            {
                if (_wordAttempt != value)
                {
                    _wordAttempt = value;
                    OnPropertyChanged();
                    VerifyWordCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string TimeElapsed
        {
            get { return _time; }
            set
            {
                _time = value;
            }

        }

        public int Point
        {
            get
            {
                return _point;
            }
            set
            {
                if (_point != value)
                {
                    _point = value;
                    OnPropertyChanged();
                }
            }
        }

        public HashSet<char> WrongPositionChar
        {
            get
            {
                return new HashSet<char>(_wrongPositionChar);
            }
            set
            {
                if (value != _wrongPositionChar)
                {
                    _wrongPositionChar = value;
                }
            }
        }
        public HashSet<char> MissingPositionChar
        {
            get
            {
                return new HashSet<char>(_missingPositionChar);
            }
            set
            {
                if (value != _missingPositionChar)
                {
                    _missingPositionChar = value;
                }
            }
        }
        #endregion

        #region Metodi
        public void Init()
        {
            _path = ConfigurationManager.AppSettings["FilePath"];
            if (_path == null)
            {
                throw new FileNotFoundException();
            }
            else
            {
                try
                {
                    List<string> wordlist = File.ReadAllLines(_path).ToList();
                    if (wordlist.Count == 0)
                    {
                        ShowErrorDialog(null);
                        _logger.Warn("Warn - the file containing the words is empty");
                    }

                    else
                    {
                        gm = new Logic(wordlist.Where(word => word.Length <= 6).ToList());
                        WinnerWord = gm.ChooseRandomWord();
                        WordLen = WinnerWord.Length;
                        _logger.Info("Info - game successfully initialized");
                        CreateList(_maxAttempt);
                    }
                }
                catch (Exception e)
                {
                    ShowErrorDialog(null);
                    _logger.Error($"Error - {e}");
                    App.Current.Shutdown();
                }
            }
        }

        private void CreateList(int attempts)
        {
            _attempts = new List<AttemptViewModel>();
            for (int i = 0; i < attempts; i++)
            {
                _attempts.Add(new AttemptViewModel(WordLen));
            }
        }

        private void VerifyWord(object? o)
        {

            if (WordAttempt != null && WinnerWord != null)
            {
                WordAttempt = WordAttempt.ToLower().Replace(" ", "");

                // Se la parola è già stata inserita
                if (!IsDuplicateWord(WordAttempt) && Regex.IsMatch(WordAttempt, @"^[a-zA-Z]+$"))
                {
                    if (WordAttempt.Length == WinnerWord.Length)
                    {

                        // Carico il turno corrente
                        AttemptViewModel attempt = Attempts.ElementAt(_currentAttempt);

                        // Inserimento parola nella GM Logic
                        gm.VerifyPosition(WordAttempt);

                        // Prendo la lista di posizioni del turno corrente e li salvo in una lista
                        List<Position> currentPositionAttempt = gm.GameBoard[_currentAttempt].Positions.ToList();

                        // Mando la parola e la lista al ViewModel dell' attempt
                        attempt.SetViewCharacter(WordAttempt, currentPositionAttempt);

                        // Caricare la lista dei caratteri
                        //_wrongPositionChar.UnionWith(attempt.WrongPositionChar);

                        _wrongPositionChar = (attempt.WrongPositionChar);
                        _missingPositionChar.UnionWith(attempt.MissingPositionChar);

                        OnPropertyChanged(nameof(WrongPositionChar));
                        OnPropertyChanged(nameof(MissingPositionChar));

                        _usedWords.Add(WordAttempt);

                        //Controllo la vittoria
                        if (gm.IsWinner(WordAttempt))
                        {
                            SetPoint();
                            ShowResultDialog(true);


                        }
                        else if (_currentAttempt >= (_maxAttempt - 1))
                        {
                            ShowResultDialog(false);
                        }
                        else
                        {
                            _currentAttempt++;
                            WordAttempt = "";
                        }
                    }
                }
                else
                {
                    // Se la parola è già presente in lista lancia l'evento
                    //  string message = !Regex.IsMatch(WordAttempt, @"^[a-zA-Z]+$") ? "Attenzione, non puoi usare numeri o caratteri speciali" : "La parola è gia stata inserita";
                    //ShowErrorDialog(message);
                    BlinkTextBox.Invoke();
                    WordAttempt = "";
                }
            }
            else
            {
                ShowErrorDialog(null);
                _logger.Error($"WordAttempt is null = {WordAttempt == null} || WinnerWord is null = {WinnerWord == null}");
            }
        }

        private bool IsDuplicateWord(string word)
        {
            return _usedWords.Contains(word);
        }

        private void ShowResultDialog(bool isWinner)
        {
            WordleDialog childwin = new((item) => { if (item) { RestartGameCommand.Execute(null); } else { Application.Current.Shutdown(); } }, WinnerWord, isWinner);
            childwin.ShowDialog();
        }

        private void ShowErrorDialog(string? ovveraidMessage)
        {
            WordleDialog errorDialog = new(Application.Current.Shutdown, ovveraidMessage);
            errorDialog.ShowDialog();
        }

        private bool CanPressed(object? o)
        {
            return WordAttempt.Length == WinnerWord.Length;
        }

        private void RestartGame(object? o)
        {
            Init();
            CreateList(_maxAttempt);
            WordAttempt = "";
            _currentAttempt = 0;
            _wrongPositionChar.Clear();
            _missingPositionChar.Clear();
            _usedWords.Clear();
            OnPropertyChanged(nameof(WinnerWord));
            OnPropertyChanged(nameof(WordLen));
            OnPropertyChanged(nameof(Attempts));
            OnPropertyChanged(nameof(WordAttempt));
            OnPropertyChanged(nameof(WrongPositionChar));
            OnPropertyChanged(nameof(MissingPositionChar));
            _logger.Info("Info - game successfully restarted");
        }

        private void SetPoint()
        {
            switch (_currentAttempt)
            {
                case 0:
                    Point += 100;
                    break;
                case 1:
                    Point += 80;
                    break;
                case 2:
                    Point += 60;
                    break;
                case 3:
                    Point += 40;
                    break;
                case 4:
                    Point += 20;
                    break;
                case 5:
                    Point += 5;
                    break;
            }
        }

        #endregion

        #region Eventi
        private void TimerElapsed(object? sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                TimeElapsed = _stopwatch.Elapsed.ToString(@"hh\:mm\:ss");
                OnPropertyChanged(nameof(TimeElapsed));
            });
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
