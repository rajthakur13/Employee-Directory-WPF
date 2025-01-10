using EmployeeDirectory.Models;
using EmployeeDirectory.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows;
using EmployeeDirectory.Views;

namespace EmployeeDirectory.ViewModels
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Employee> _employees;

        private Employee _selectedEmployee;

        private string _name;
        private string _email;
        private string _position;
        private string _department;

        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            set
            {
                _employees = value;
                OnPropertyChanged(nameof(Employees));
            }
        }

        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Position
        {
            get { return _position; }
            set
            {
                _position = value;
                OnPropertyChanged(nameof(Position));
            }
        }

        public string Department
        {
            get { return _department; }
            set
            {
                _department = value;
                OnPropertyChanged(nameof(Department));
            }
        }


        public ICommand AddEmployeeCommand { get; set; }
        public ICommand EditEmployeeCommand { get; set; }
        public ICommand DeleteEmployeeCommand { get; set; }

        public EmployeeViewModel()
        {
            Employees = new ObservableCollection<Employee>();
            AddEmployeeCommand = new RelayCommand(OpenAddEmployeeWindow);
            EditEmployeeCommand = new RelayCommand(OpenEditEmployeeWindow, CanEditOrDelete);
            DeleteEmployeeCommand = new RelayCommand(DeleteEmployee, CanEditOrDelete);
        }

        private bool CanEditOrDelete()
        {
            return SelectedEmployee != null;
        }

        private void OpenAddEmployeeWindow()
        {
            var addEmployeeWindow = new EmployeePopupWindow(SubmitNewEmployee);
            addEmployeeWindow.ShowDialog();
        }

        private void OpenEditEmployeeWindow()
        {
            if (SelectedEmployee == null)
                return;

            var editEmployeeWindow = new EmployeePopupWindow(SubmitEditedEmployee)
            {
                DataContext = new EmployeePopupViewModel(
                               SubmitEditedEmployee,
                               SelectedEmployee.Name,
                               SelectedEmployee.Email,
                               SelectedEmployee.Position,
                               SelectedEmployee.Department)
            };
            editEmployeeWindow.ShowDialog();
        }


        public void SubmitNewEmployee(string name, string email, string position, string department)
        {
            var employee = new Employee
            {
                Id = Employees.Count + 1,
                Name = name,
                Email = email,
                Position = position,
                Department = department
            };
            Employees.Add(employee);
        }


        private void SubmitEditedEmployee(string name, string email, string position, string department)
        {
            if (SelectedEmployee != null)
            {
                SelectedEmployee.Name = name;
                SelectedEmployee.Email = email;
                SelectedEmployee.Position = position;
                SelectedEmployee.Department = department;
            }
        }


        private void AddEmployee()
        {
            var employee = new Employee
            {
                Id = Employees.Count + 1,
                Name = Name,
                Email = Email,
                Position = Position,
                Department = Department
            };
            Employees.Add(employee);
            ClearFields();
        }

        private void EditEmployee()
        {
            if (SelectedEmployee != null)
            {
                Name = SelectedEmployee.Name;
                Email = SelectedEmployee.Email;
                Position = SelectedEmployee.Position;
                Department = SelectedEmployee.Department;
            }
        }

        private void DeleteEmployee()
        {
            if (SelectedEmployee != null)
            {
                var confirmResult = MessageBox.Show("Are you sure to delete this employee?", "Confirm Delete", MessageBoxButton.YesNo);
                if (confirmResult == MessageBoxResult.Yes)
                {
                    Employees.Remove(SelectedEmployee);
                }
            }
        }
        private void ClearFields()
        {
            Name = string.Empty;
            Email = string.Empty;
            Position = string.Empty;
            Department = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
