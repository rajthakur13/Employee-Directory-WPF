using EmployeeDirectory.Models;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmployeeDirectory
{

    public partial class MainWindow : Window
    {
        private EmployeeViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new EmployeeViewModel();
            DataContext = _viewModel;
        }

        public bool IsValidEmail(string email)
        {
            var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailPattern);
        }

        private void OnAddEmployeeClick(object sender, RoutedEventArgs e)
        {
            _viewModel.SelectedEmployee = null;

            var name = NameTextBox.Text;
            var position = PositionTextBox.Text;
            var department = DepartmentTextBox.Text;
            var email = EmailTextBox.Text;

            var existingEmployee = _viewModel.Employees.FirstOrDefault(e => e.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            if (existingEmployee != null)
            {
                MessageBox.Show("An employee with this email already exists.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(position) ||
                string.IsNullOrWhiteSpace(department) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("All fields must be filled.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newEmployee = new Employee
            {
                Name = name,
                Position = position,
                Department = department,
                Email = email
            };

            _viewModel.AddEmployee(newEmployee);
            ClearTextBoxes();

        }

        private void OnUpdateEmployeeClick(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedEmployee != null)
            {

                var name = NameTextBox.Text;
                var position = PositionTextBox.Text;
                var department = DepartmentTextBox.Text;
                var email = EmailTextBox.Text;

                if (!IsValidEmail(email))
                {
                    MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var existingEmployee = _viewModel.Employees.FirstOrDefault(e => e.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && e.Id != _viewModel.SelectedEmployee.Id);
                if (existingEmployee != null)
                {
                    MessageBox.Show("An employee with this email already exists.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(position) ||
                    string.IsNullOrWhiteSpace(department) || string.IsNullOrWhiteSpace(email))
                {
                    MessageBox.Show("All fields must be filled.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                _viewModel.SelectedEmployee.Name = name;
                _viewModel.SelectedEmployee.Position = position;
                _viewModel.SelectedEmployee.Department = department;
                _viewModel.SelectedEmployee.Email = email;

                _viewModel.UpdateEmployee(_viewModel.SelectedEmployee);

                DataGrid.Items.Refresh();
            }
            ClearTextBoxes();
            _viewModel.SelectedEmployee = null;
        }

        private void OnDeleteEmployeeClick(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedEmployee != null)
            {
                _viewModel.DeleteEmployee(_viewModel.SelectedEmployee);
            }
            _viewModel.SelectedEmployee = null;
        }

        private void ClearTextBoxes()
        {
            NameTextBox.Clear();
            PositionTextBox.Clear();
            DepartmentTextBox.Clear();
            EmailTextBox.Clear();
        }

        private void OnDeselectEmployeeClick(object sender, RoutedEventArgs e)
        {
            ClearTextBoxes();
            _viewModel.SelectedEmployee = null;
        }
    }
}