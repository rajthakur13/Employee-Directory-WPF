using EmployeeDirectory.Core.Services;
using EmployeeDirectory.Core.Models;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDirectory.Core.ViewModels
{
    public class EmployeeViewModel : MvxViewModel
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeViewModel(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            await LoadEmployees();
        }

        private ObservableCollection<Employee> _employees;
        public ObservableCollection<Employee> Employees
        {
            get => _employees;
            set
            {
                _employees = value;
                RaisePropertyChanged(() => Employees);
            }
        }

        private string _newEmployeeName;
        public string NewEmployeeName
        {
            get => _newEmployeeName;
            set
            {
                _newEmployeeName = value;
                RaisePropertyChanged(() => NewEmployeeName);
            }
        }

        private string _newEmployeeEmail;
        public string NewEmployeeEmail
        {
            get => _newEmployeeEmail;
            set
            {
                _newEmployeeEmail = value;
                RaisePropertyChanged(() => NewEmployeeEmail);
            }
        }

        private string _newEmployeeDepartment;
        public string NewEmployeeDepartment
        {
            get => _newEmployeeDepartment;
            set
            {
                _newEmployeeDepartment = value;
                RaisePropertyChanged(() => NewEmployeeDepartment);
            }
        }

        public IMvxCommand LoadEmployeesCommand => new MvxCommand(async () => await LoadEmployees());
        private async Task LoadEmployees()
        {
            var employeeList = await _employeeService.GetAllEmployees();
            Employees = new ObservableCollection<Employee>(employeeList);
        }

        public IMvxCommand AddEmployeeCommand => new MvxCommand(async () => await AddEmployee());

        private async Task AddEmployee()
        {
            if (string.IsNullOrWhiteSpace(NewEmployeeName) ||
                string.IsNullOrWhiteSpace(NewEmployeeEmail) ||
                string.IsNullOrWhiteSpace(NewEmployeeDepartment))
            {
                Console.Write("Error Occured");
                return;
            }

            var newEmployee = new Employee
            {
                Name = NewEmployeeName,
                Email = NewEmployeeEmail,
                Department = NewEmployeeDepartment
            };

            var addedEmployeeId = await _employeeService.AddEmployee(newEmployee);

            if (addedEmployeeId > 0)
            {
                await LoadEmployees();
                NewEmployeeName = string.Empty;
                NewEmployeeEmail = string.Empty;
                NewEmployeeDepartment = string.Empty;
            }
            else
            {
                Console.Write("Error Occured");
            }
        }
    }
}
