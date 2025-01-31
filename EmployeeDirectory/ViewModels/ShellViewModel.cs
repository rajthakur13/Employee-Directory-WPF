using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDirectory.WPF.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        public ShellViewModel() { }

        protected async override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await EmployeeViewload();
        }

        public async Task EmployeeViewload()
        {
            var viewmodel = IoC.Get<EmployeeViewModel>();
            await ActivateItemAsync(viewmodel, new CancellationToken());
        }
    }
}