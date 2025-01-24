using CommunityToolkit.Mvvm.DependencyInjection;
using EmployeeDirectory.Core.Services;
using EmployeeDirectory.Core.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeDirectory.WPF
{
    public static class Setup
    {
        public static void ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IEmployeeService, EmployeeService>();
            serviceCollection.AddSingleton<INavigationService, NavigationService>();

            serviceCollection.AddTransient<EmployeeViewModel>();
            serviceCollection.AddTransient<AddEmployeeViewModel>();

            Ioc.Default.ConfigureServices(serviceCollection.BuildServiceProvider());
        }
    }
}
