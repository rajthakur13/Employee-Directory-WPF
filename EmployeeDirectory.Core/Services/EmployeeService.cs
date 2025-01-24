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
        private readonly Dictionary<int, Employee> _employees = new();

        public Task<List<Employee>> GetAllEmployeesAsync()
        {
            return Task.FromResult(_employees.Values.ToList());
        }

        public Task<int> AddEmployeeAsync(Employee employee)
        {
            employee.Id = _employees.Count > 0 ? _employees.Keys.Max() + 1 : 1;
            _employees[employee.Id] = employee;
            return Task.FromResult(employee.Id);
        }

        public Task<bool> DeleteEmployeeAsync(int employeeId) 
        {
            if(_employees.Remove(employeeId))
            {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<bool> UpdateEmployeeAsync(Employee updateEmployee)
        {
            if (_employees.ContainsKey(updateEmployee.Id))
            {
                _employees[updateEmployee.Id] = updateEmployee;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
