using EmployeeDirectory.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDirectory.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly List<Employee> _employees = new();

        public Task<List<Employee>> GetAllEmployees()
        {
            return Task.FromResult(_employees);
        }

        public Task<int> AddEmployee(Employee employee)
        {
            employee.Id = _employees.Any() ? _employees.Max(e => e.Id) + 1 : 1;
            _employees.Add(employee);
            return Task.FromResult(employee.Id);
        }

        public Task<bool> DeleteEmployee(int employeeId) 
        {
            var employeeToRemove = _employees.FirstOrDefault(e => e.Id == employeeId);
            if (employeeToRemove != null)
            {
                _employees.Remove(employeeToRemove);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<bool> UpdateEmployee(Employee updateEmployee)
        {
            var existingEmployee = _employees.FirstOrDefault(e => e.Id == updateEmployee.Id);
            if (existingEmployee != null)
            {
                existingEmployee.Name = updateEmployee.Name;
                existingEmployee.Email = updateEmployee.Email;
                existingEmployee.Position = updateEmployee.Position;
                existingEmployee.Department = updateEmployee.Department;

                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
