using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDirectory.Core.Services
{
    public interface INavigationService
    {
        Task CloseAsync();
        Task NavigateToAsync<TViewModel>(object parameter = null) where TViewModel : class;
    }
}
