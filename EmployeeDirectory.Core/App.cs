using EmployeeDirectory.Core.Services;
using MvvmCross.ViewModels;
using MvvmCross;
using EmployeeDirectory.Core.ViewModels;

namespace EmployeeDirectory.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.RegisterType<IEmployeeService, EmployeeService>();
            RegisterAppStart<EmployeeViewModel>();
        }
    }
}
