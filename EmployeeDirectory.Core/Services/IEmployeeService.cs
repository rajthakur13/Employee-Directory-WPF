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
        Task<List<Employee>> GetAllEmployees();
        Task<int> AddEmployee(Employee employee);
    }
}
