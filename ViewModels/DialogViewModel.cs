﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace WordleWPF.ViewModels
{
    public class DialogViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private string _paragraph = "";
        private readonly Action<bool> _actBool;


        public DelegateCommand YesButtonCommand { get; }
        public DelegateCommand NoButtonCommand { get; }

        public DialogViewModel(Action<bool> actBool, string WinnerWord, bool winner)
        {
            _actBool = actBool;
            Paragraph = $"{"Hai " + (winner ? "vinto" : "perso")}" + $"{(winner ? "" : (" la parola corretta è " + $"{WinnerWord.ToUpper()}"))}" + $"{", vuoi giorcare ancora?"}";
            YesButtonCommand = new(YesFunction);
            NoButtonCommand = new(NoFunction);
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

        private void YesFunction(object? o)
        {
            _actBool(true);
        }

        private void NoFunction(object? o)
        {
            _actBool(false);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
