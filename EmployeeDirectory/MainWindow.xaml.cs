using EmployeeDirectory.Models;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

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
            var addEmployeeWindow = new EmployeePopupWindow();
            if (addEmployeeWindow.ShowDialog() == true)
            {
                _viewModel.AddEmployee(addEmployeeWindow.Employee);
            }
        }

        private void OnDeleteEmployeeClick(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedEmployee != null)
            {
                var result = MessageBox.Show(
                    $"Are you sure you want to delete employee {_viewModel.SelectedEmployee.Name}?",
                    "Delete Confirmation",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    _viewModel.DeleteEmployee(_viewModel.SelectedEmployee);
                }
            }
            else
            {
                MessageBox.Show("Please select an employee to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void OnEditClick(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedEmployee == null)
            {
                MessageBox.Show("Please select an employee to edit.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var editEmployeeWindow = new EmployeePopupWindow(_viewModel.SelectedEmployee, isEditMode: true);
            if (editEmployeeWindow.ShowDialog() == true)
            {
                _viewModel.UpdateEmployee(editEmployeeWindow.Employee);
            }
        }

    }
}
