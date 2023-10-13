using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using WordleWPF.View;
using WordleWPF.ViewModel;

namespace WordleWPF.ViewModels
{
    public class DialogViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private string _title = "DialogBox";

        public DelegateCommand RestartGameCommand { get; }
        public DialogViewModel()
        {
            RestartGameCommand = new DelegateCommand(RestartGame);
        }

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

        private void RestartGame(object? o)
        {
            //Application.Current.MainWindow.IsEnabled = true;
            MainViewModel vm = new();
            vm.RestartGameCommand.Execute(null);
            //Application.Current.Windows.OfType<WordleDialog>().SingleOrDefault()?.Close();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
