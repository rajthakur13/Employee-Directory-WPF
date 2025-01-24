using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace EmployeeDirectory.Core.Models
{
    public partial class Employee : ObservableObject
    {
        public int Id { get; set; }

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _position;

        [ObservableProperty]
        private string _department;
    }
}
