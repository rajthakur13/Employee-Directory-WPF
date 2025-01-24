using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDirectory.Core.Services
{
    public class NavigationService : INavigationService
    {

        public Task CloseAsync()
        {
            Console.WriteLine("Closing current view");
            return Task.CompletedTask;
        }


        public Task NavigateToAsync<TViewModel>(object parameter = null) where TViewModel : class
        {
            Console.WriteLine($"Navigating to {typeof(TViewModel).Name}");

            return Task.CompletedTask;
        }
    }
}
