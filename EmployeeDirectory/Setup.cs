using Microsoft.Extensions.Logging;
using MvvmCross.Platforms.Wpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDirectory.WPF
{
    public class Setup : MvxWpfSetup<Core.App>
    {
        protected override ILoggerFactory? CreateLogFactory() => default!;

        protected override ILoggerProvider? CreateLogProvider() => default!;
    }
}
