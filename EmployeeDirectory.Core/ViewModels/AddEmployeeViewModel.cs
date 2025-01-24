using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EmployeeDirectory.Core.Models;
using EmployeeDirectory.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDirectory.Core.ViewModels
{
    public class AddEmployeeViewModel : ObservableObject
    {
        private readonly IEmployeeService employeeService;
        private readonly INavigationService _navigationService;
        private EmployeeViewModel employeeViewModel;
        private Employee editingEmployee;

        public AddEmployeeViewModel(IEmployeeService employeeService, INavigationService navigationService)
        {
            this.employeeService = employeeService;
            this._navigationService = navigationService;
        }

        public void Prepare(EmployeeViewModel parameter)
        {
            employeeViewModel = parameter;
        }

        public void PrepareForEdit(Employee employee)
        {
            editingEmployee = employee;
            NewEmployeeName = employee.Name;
            NewEmployeeEmail = employee.Email;
            NewEmployeePosition = employee.Position;
            NewEmployeeDepartment = employee.Department;
        }

        private string _newEmployeeName;
        public string NewEmployeeName
        {
            get => _newEmployeeName;
            set => SetProperty(ref _newEmployeeName, value);
        }

        private string _newEmployeeEmail;
        public string NewEmployeeEmail
        {
            get => _newEmployeeEmail;
            set => SetProperty(ref _newEmployeeEmail, value);

        }

        private string _newEmployeePosition;
        public string NewEmployeePosition
        {
            get => _newEmployeePosition;
            set => SetProperty(ref _newEmployeePosition, value);

        }

        private string _newEmployeeDepartment;
        public string  NewEmployeeDepartment
        {
            get => _newEmployeeDepartment;
            set => SetProperty(ref _newEmployeeDepartment, value);
        }

        public IAsyncRelayCommand SubmitEmployeeCommand => new AsyncRelayCommand(SubmitEmployee);

        private async Task SubmitEmployee()
        {
            if (string.IsNullOrWhiteSpace(NewEmployeeName) ||
                 string.IsNullOrWhiteSpace(NewEmployeeEmail) ||
                 string.IsNullOrWhiteSpace(NewEmployeeDepartment) ||
                 string.IsNullOrWhiteSpace(NewEmployeePosition))
            {
                Console.WriteLine("Error: All fields are required.");
                return;
            }

            if (editingEmployee != null)
            {
                editingEmployee.Name = NewEmployeeName;
                editingEmployee.Email = NewEmployeeEmail;
                editingEmployee.Position = NewEmployeePosition;
                editingEmployee.Department = NewEmployeeDepartment;

                var success = await employeeService.UpdateEmployeeAsync(editingEmployee);
                if (success)
                {
                    var index = employeeViewModel.Employees.IndexOf(editingEmployee);
                    if (index >= 0)
                    {
                        employeeViewModel.Employees[index] = editingEmployee; 
                    }
                    await _navigationService.CloseAsync();
                }
                else
                {
                    Console.WriteLine("Error Occured");
                }
            }
            else
            {
                var newEmployee = new Employee
                {
                    Name = NewEmployeeName,
                    Email = NewEmployeeEmail,
                    Position = NewEmployeePosition,
                    Department = NewEmployeeDepartment
                };

                var addedEmployeeId = await employeeService.AddEmployeeAsync(newEmployee);

                if (addedEmployeeId > 0)
                {
                    await _navigationService.CloseAsync();
                    employeeViewModel.Employees.Add(newEmployee);
                }
                else
                {
                    Console.WriteLine("Error Occured!");
                }
            }
        }

        public IAsyncRelayCommand CancelCommand => new AsyncRelayCommand(async () => await Cancel());
        private async Task Cancel()
        {
            await _navigationService.CloseAsync();
        }

    }
}
