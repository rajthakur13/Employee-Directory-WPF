using MvvmCross.Platforms.Wpf.Core;  // Ensure this is the correct namespace for MVVM Cross in WPF
using MvvmCross.ViewModels;
using EmployeeDirectory.Core.ViewModels;  // Replace with your actual Core ViewModel namespace

namespace EmployeeDirectory.UI
{
    public partial class App : MvxApplication  // Ensure this class inherits from MvxApplication
    {
        public App()
        {
            Initialize();  // Optional initialization logic
        }

        public override void Initialize()
        {
            // Register the initial ViewModel that will be shown when the app starts
            RegisterAppStart<EmployeeViewModel>();  // Replace with your actual ViewModel
        }
    }
}
