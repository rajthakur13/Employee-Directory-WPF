using CommunityToolkit.Mvvm.DependencyInjection;
using EmployeeDirectory.Core.ViewModels;
using System.Windows;

namespace EmployeeDirectory.WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var employeeViewModel = Ioc.Default.GetService<EmployeeViewModel>();
            DataContext = employeeViewModel;
        }
    }
}
