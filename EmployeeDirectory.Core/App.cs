using EmployeeDirectory.Core.Services;
using EmployeeDirectory.Core.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EmployeeDirectory.Core
{
    public class App
    {
        private static IServiceProvider _serviceProvider;

        public static void Initialize()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            _serviceProvider = serviceCollection.BuildServiceProvider();

            var initialViewModel = _serviceProvider.GetRequiredService<EmployeeViewModel>();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IEmployeeService, EmployeeService>();

            services.AddTransient<EmployeeViewModel>();
            services.AddTransient<AddEmployeeViewModel>();

        }
    }
}