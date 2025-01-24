using EmployeeDirectory.Core.Services;
using EmployeeDirectory.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace EmployeeDirectory.Core.ViewModels
{
    public class EmployeeViewModel : ObservableObject
    {
        private readonly IEmployeeService _employeeService;
        private readonly INavigationService _navigationService;

        public EmployeeViewModel(IEmployeeService employeeService, INavigationService navigationService)
        {
            _employeeService = employeeService;
            _navigationService = navigationService;
            LoadEmployeesCommand.ExecuteAsync(null);
        }

        private ObservableCollection<Employee> _employees;
        public ObservableCollection<Employee> Employees
        {
            get => _employees;
            set => SetProperty(ref _employees, value);
        }
        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set => SetProperty(ref _selectedEmployee, value);
        }


        public IAsyncRelayCommand LoadEmployeesCommand => new AsyncRelayCommand(LoadEmployees);
        public async Task LoadEmployees()
        {
            var employeeList = await _employeeService.GetAllEmployeesAsync();
            Employees = new ObservableCollection<Employee>(employeeList);
        }

        public IAsyncRelayCommand OpenAddEmployeePopupCommand => new AsyncRelayCommand(OpenAddEmployeePopup);

        private async Task OpenAddEmployeePopup()
        {
            await _navigationService.NavigateToAsync<AddEmployeeViewModel>(this);
        }

        public IAsyncRelayCommand DeleteEmployeeCommand => new AsyncRelayCommand(DeleteEmployee);
        private async Task DeleteEmployee()
        {
            if(SelectedEmployee != null)
            {
                var success = await _employeeService.DeleteEmployeeAsync(SelectedEmployee.Id);
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

        public IAsyncRelayCommand<Employee> EditEmployeeCommand => new AsyncRelayCommand<Employee>(async employee => await EditEmployee(SelectedEmployee));

        private async Task EditEmployee(Employee employee)
        {
            var addEmployeeViewModel = Ioc.Default.GetService<AddEmployeeViewModel>();
            addEmployeeViewModel.PrepareForEdit(employee);
            await _navigationService.NavigateToAsync<AddEmployeeViewModel>(employee);
        }
    }
}
