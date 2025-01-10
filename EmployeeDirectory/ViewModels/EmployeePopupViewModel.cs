using EmployeeDirectory.Models;
using EmployeeDirectory.Commands;
using System;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;

namespace EmployeeDirectory.ViewModels
{
    public class EmployeePopupViewModel : INotifyPropertyChanged
    {
        public ICommand SubmitCommand { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }

        private readonly Action<string, string, string, string> _submitEmployeeCallback;

        public EmployeePopupViewModel(Action<string, string, string, string> submitEmployeeCallback, string name = null, string email = null, string position = null, string department = null)
        {
            _submitEmployeeCallback = submitEmployeeCallback;
            Name = name;
            Email = email;
            Position = position;
            Department = department;

            SubmitCommand = new RelayCommand(Submit);
        }

        private void Submit()
        {
            if (_submitEmployeeCallback != null)
            {
                _submitEmployeeCallback(Name, Email, Position, Department);
                ((Window)Application.Current.Windows[1]).Close();
            }

            ((Window)Application.Current.Windows[1]).Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
