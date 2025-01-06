using EmployeeDirectory.Models;
using System.Windows;

namespace EmployeeDirectory
{
    public partial class EmployeePopupWindow : Window
    {
        public Employee Employee { get; private set; }
        public string Title { get; set; }
        public string HeaderText { get; set; }
        public string SaveButtonText { get; set; }

        public EmployeePopupWindow(Employee employee = null, bool isEditMode = false)
        {
            InitializeComponent();

            Title = isEditMode ? "Edit Employee" : "Add Employee";
            HeaderText = isEditMode ? "Edit Employee Details" : "Add New Employee";
            SaveButtonText = isEditMode ? "Update" : "Add";

            Employee = employee != null
                ? new Employee
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Position = employee.Position,
                    Department = employee.Department,
                    Email = employee.Email
                }
                : new Employee();

            DataContext = this;
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Employee.Name) ||
               string.IsNullOrWhiteSpace(Employee.Email))
            {
                MessageBox.Show("Please fill in all fields before saving.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!IsValidEmail(Employee.Email))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DialogResult = true;
            Close();
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern);
        }
    }
}
