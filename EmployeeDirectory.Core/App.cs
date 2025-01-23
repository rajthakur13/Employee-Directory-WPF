using EmployeeDirectory.Core.Services;
using MvvmCross.ViewModels;
using MvvmCross;
using EmployeeDirectory.Core.ViewModels;
using MvvmCross.IoC;

namespace EmployeeDirectory.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            //Mvx.IoCProvider.RegisterType<IEmployeeService, EmployeeService>();
            Mvx.IoCProvider.RegisterSingleton<IEmployeeService>(new EmployeeService());
            Mvx.IoCProvider.RegisterType<AddEmployeeViewModel>();
            RegisterAppStart<EmployeeViewModel>();
        }
    }
}
