using System;
using System.Windows;
using EmployeeDirectory.ViewModels;

namespace EmployeeDirectory.Views
{
    public partial class EmployeePopupWindow : Window
    {
        public EmployeePopupWindow(Action<string, string, string, string> submitEmployeeCallback)
        {
            InitializeComponent();

            var viewModel = new EmployeePopupViewModel(submitEmployeeCallback);
            DataContext = viewModel;
        }
    }
}
