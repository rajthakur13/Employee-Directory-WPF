using EmployeeDirectory.Core.Models;
using EmployeeDirectory.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDirectory.Core.ViewModels
{
    public class AddEmployeeViewModel : MvxViewModel<EmployeeViewModel>
    {
        private readonly IEmployeeService employeeService;
        private readonly IMvxNavigationService navigationService;
        private EmployeeViewModel employeeViewModel;
        private Employee editingEmployee;

        public AddEmployeeViewModel(IEmployeeService employeeService, IMvxNavigationService navigationService)
        {
            this.employeeService = employeeService;
            this.navigationService = navigationService;
        }

        public override void Prepare(EmployeeViewModel parameter)
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
            get
            {
                return _newEmployeeName;
            }
            //set
            //{
            //    _newEmployeeName = value;
            //    RaisePropertyChanged(() => NewEmployeeName);
            //}
            set => SetProperty(ref _newEmployeeName, value);
        }

        private string _newEmployeeEmail;
        public string NewEmployeeEmail
        {
            get
            {
                return _newEmployeeEmail;
            }
            //set
            //{
            //    _newEmployeeEmail = value;
            //    RaisePropertyChanged(() => NewEmployeeEmail);
            //}
            set => SetProperty(ref _newEmployeeEmail, value);

        }

        private string _newEmployeePosition;
        public string NewEmployeePosition
        {
            get
            {
                return _newEmployeePosition;
            }
            set => SetProperty(ref _newEmployeePosition, value);

        }

        private string _newEmployeeDepartment;
        public string  NewEmployeeDepartment
        {
            get
            {
                return _newEmployeeDepartment;
            }
            //set
            //{
            //    _newEmployeeDepartment = value;
            //    RaisePropertyChanged(() => NewEmployeeDepartment);
            //}
            set => SetProperty(ref _newEmployeeDepartment, value);

        }

        public IMvxCommand SubmitEmployeeCommand => new MvxCommand(async () => await SubmitEmployee());

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

                var success = await employeeService.UpdateEmployee(editingEmployee);
                if (success)
                {
                    //var index = employeeViewModel.Employees.IndexOf(editingEmployee);
                    //if (index >= 0)
                    //{
                    //    employeeViewModel.Employees[index] = editingEmployee;
                    //}
                    await navigationService.Close(this);
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

                var addedEmployeeId = await employeeService.AddEmployee(newEmployee);

                if (addedEmployeeId > 0)
                {
                    await navigationService.Close(this);
                    employeeViewModel.Employees.Add(newEmployee);
                }
                else
                {
                    Console.WriteLine("Error Occured!");
                }
            }
        }

        public IMvxCommand CancelCommand => new MvxCommand(async () => await Cancel());
        private async Task Cancel()
        {
          await navigationService.Close(this); 
        }

    }
}
