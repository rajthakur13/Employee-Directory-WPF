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
    }
}
