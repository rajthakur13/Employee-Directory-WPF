using Caliburn.Micro;
using EmployeeDirectory.WPF.Models;
using EmployeeDirectory.WPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDirectory.WPF.ViewModels
{
    public class AddEmployeeViewModel : Screen
    {
        private readonly EmployeeService _employeeService;
        private readonly Employee _existingEmployee;

        public bool IsEditMode { get; private set; }
        public string EmployeeName { get; set; }
        public string EmployeePosition { get; set; }
        public string EmployeeDepartment { get; set; }
        public string EmployeeEmail { get; set; }
        public AddEmployeeViewModel(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public AddEmployeeViewModel(EmployeeService employeeService, Employee employee)
        {
            _employeeService = employeeService;
            _existingEmployee = employee;

            EmployeeName = employee.Name;
            EmployeePosition = employee.Position;
            EmployeeDepartment = employee.Department;
            EmployeeEmail = employee.Email;
            IsEditMode = true;
        }

        public void Submit()
        {
            if (string.IsNullOrWhiteSpace(EmployeeName) || string.IsNullOrWhiteSpace(EmployeePosition))
            {
                return;
            }

            if (IsEditMode && _existingEmployee != null)
            {
                _existingEmployee.Name = EmployeeName;
                _existingEmployee.Position = EmployeePosition;
                _existingEmployee.Department = EmployeeDepartment;
                _existingEmployee.Email = EmployeeEmail;
                _employeeService.UpdateEmployee(_existingEmployee);
            }
            else
            {
                var nextId = _employeeService.GetAllEmployees().Any()
                    ? _employeeService.GetAllEmployees().Max(e => e.Id) + 1
                    : 1;

                var newEmployee = new Employee
                {
                    Id = nextId,
                    Name = EmployeeName,
                    Position = EmployeePosition,
                    Department = EmployeeDepartment,
                    Email = EmployeeEmail
                };

                _employeeService.AddEmployee(newEmployee);
            }

            TryCloseAsync();
        }

        public void Cancel()
        {
            TryCloseAsync();
        }
    }
}
