using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeDirectory.Core.Models;


namespace EmployeeDirectory.Core.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<int> AddEmployeeAsync(Employee employee);
        Task<bool> DeleteEmployeeAsync(int employeeId);
        Task<bool> UpdateEmployeeAsync(Employee updateEmployee);
    }
}
