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
using MvvmCross.Navigation;
using MvvmCross;

namespace EmployeeDirectory.Core.ViewModels
{
    public class EmployeeViewModel : MvxViewModel
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMvxNavigationService _navigationService;

        public EmployeeViewModel(IEmployeeService employeeService, IMvxNavigationService navigationService)
        {
            _employeeService = employeeService;
            _navigationService = navigationService;
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
        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                RaisePropertyChanged(() => SelectedEmployee);
            }
        }


        public IMvxCommand LoadEmployeesCommand => new MvxCommand(async () => await LoadEmployees());
        public async Task LoadEmployees()
        {
            var employeeList = await _employeeService.GetAllEmployees();
            Employees = new ObservableCollection<Employee>(employeeList);
        }

        public IMvxCommand OpenAddEmployeePopupCommand => new MvxCommand(async () => await OpenAddEmployeePopup());

        private async Task OpenAddEmployeePopup()
        {
            await _navigationService.Navigate<AddEmployeeViewModel, EmployeeViewModel>(this);
        }

        public IMvxCommand DeleteEmployeeCommand => new MvxCommand(async () => await DeleteEmployee());
        private async Task DeleteEmployee()
        {
            if(SelectedEmployee != null)
            {
                var success = await _employeeService.DeleteEmployee(SelectedEmployee.Id);
                if (success)
                {
                    Employees.Remove(SelectedEmployee);
                    SelectedEmployee = null;
                }
                else
                {
                    Console.WriteLine("Error Occured!");
                }
            }
        }

        public IMvxCommand<Employee> EditEmployeeCommand => new MvxCommand<Employee>(async employee => await EditEmployee(SelectedEmployee));

        private async Task EditEmployee(Employee employee)
        {
            var addEmployeeViewModel = Mvx.IoCProvider.Resolve<AddEmployeeViewModel>();
            addEmployeeViewModel.PrepareForEdit(employee);
            await _navigationService.Navigate(addEmployeeViewModel);
        }
    }
}
