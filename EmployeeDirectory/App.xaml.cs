using EmployeeDirectory.WPF;
using System.Configuration;
using EmployeeDirectory.Core.Services;
using EmployeeDirectory.Core.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace EmployeeDirectory
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Setup.ConfigureServices();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindowViewModel = Ioc.Default.GetService <EmployeeViewModel>();
            var mainWindow = new MainWindow
            {
                DataContext = mainWindowViewModel
            };
            mainWindow.Show();
        }

    }

}
