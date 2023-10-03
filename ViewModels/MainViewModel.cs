using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows;
using Wordle_Library;
using Wordle_Library.Enum;
using WordleWPF.ViewModels;

namespace WordleWPF.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Variabili
        Logic? gm;
        public event PropertyChangedEventHandler? PropertyChanged;
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        public const int MaxAttempt = 6;
        private int _currentAttempt = 0;
        private string? _path;
        private string? _winnerWord;
        private List<AttemptViewModel>? _attempts;
        private List<char> _wrongPositionChar;
        private List<char> _missingPositionChar;
        private string? _wordAttempt;
        private readonly Stopwatch _stopwatch;
        private readonly Timer _interval;
        private string _time = "00:00:00";
        private int _point = 0;
        public DelegateCommand RestartGameCommand { get; }
        public DelegateCommand VerifyWordCommand { get; }
        #endregion

        #region Costruttore
        public MainViewModel()
        {
            RestartGameCommand = new DelegateCommand(RestarGame);
            VerifyWordCommand = new DelegateCommand(VerifyWord);
            _wrongPositionChar = new();
            _missingPositionChar = new();
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
                return _winnerWord.Length;
            }

        }

        public string? WordAttempt
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
                    OnPropertyChanged(nameof(Point));
                }
            }
        }

        public List<char> WrongPositionChar
        {
            get
            {
                return new List<char>(_wrongPositionChar);
            }
            set
            {
                if (value != _wrongPositionChar)
                {
                    _wrongPositionChar = value;
                }
            }
        }
        public List<char> MissingPositionChar
        {
            get
            {
                return new List<char>(_missingPositionChar);
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
                        MessageBox.Show("Could not start the game because of an unexpected error, please contact the technical support.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        _logger.Warn("Warn - the file containing the words is empty");
                        App.Current.Shutdown();
                    }

                    else
                    {
                        gm = new Logic(wordlist);
                        WinnerWord = gm.ChooseRandomWord();
                        _logger.Info("Info - game successfully initialized");
                        CreateList(MaxAttempt);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("An internal error occured, please contact the technical support");
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
                _attempts.Add(new AttemptViewModel(_winnerWord));
            }
        }

        private void VerifyWord(object? o)
        {
            if (WordAttempt != null && WinnerWord != null)
            {
                WordAttempt = WordAttempt.ToLower().Replace(" ", ""); ;
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
                    _wrongPositionChar.AddRange(attempt.WrongPositionChar);
                    _missingPositionChar.AddRange(attempt.MissingPositionChar);

                    OnPropertyChanged(nameof(WrongPositionChar));
                    OnPropertyChanged(nameof(MissingPositionChar));

                    //Aggiorno la View
                    OnPropertyChanged(nameof(Attempts));

                    //Controllo la vittoria
                    if (gm.IsWinner(WordAttempt))
                    {
                        SetPoint();
                        MessageBoxResult result = MessageBox.Show("Do you wanna play again", "You Win", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                RestarGame(null);
                                break;
                            case MessageBoxResult.No:
                                App.Current.Shutdown();
                                break;
                        }
                    }
                    else if (_currentAttempt >= (MaxAttempt - 1))
                    {
                        MessageBoxResult result = MessageBox.Show($"The correct word was '{WinnerWord.ToUpper()}'", "You Lose", MessageBoxButton.OK, MessageBoxImage.Error);
                        switch (result)
                        {
                            case MessageBoxResult.OK:
                                App.Current.Shutdown();
                                break;
                        }
                    }
                    else
                    {
                        _currentAttempt++;
                    }
                }
                else
                {
                    MessageBox.Show($"The word entered is too {(WordAttempt.Length > WinnerWord.Length ? "Long" : "Short")}", "Length Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("An internal error occured, please contact the technical support", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _logger.Error($"WordAttempt is null = {WordAttempt == null} || WinnerWord is null = {WinnerWord == null}");
            }
        }

        private void RestarGame(object? o)
        {
            Init();
            CreateList(MaxAttempt);
            WordAttempt = null;
            _currentAttempt = 0;
            _wrongPositionChar.Clear();
            _missingPositionChar.Clear();
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
