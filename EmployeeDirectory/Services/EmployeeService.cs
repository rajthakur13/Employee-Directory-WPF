using EmployeeDirectory.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDirectory.WPF.Services
{
    public class EmployeeService
    {
        private readonly List<Employee> _employees = new List<Employee>();

        public EmployeeService()
        {

            _employees.Add(new Employee { Id = 1, Name = "Jim Carter", Position = "Software Engineer", Email = "jim.c@pravaltech.com", Department = "IT" });
            _employees.Add(new Employee { Id = 2, Name = "Alex Murphy", Position = "Product Manager", Email = "alex.m@pravaltech.com", Department = "Buisness" });
            _employees.Add(new Employee { Id = 3, Name = "Megan Bowen", Position = "Designer", Email = "megan.bown@pravaltech.com", Department = "Branding" });
            _employees.Add(new Employee { Id = 4, Name = "Zack Nestor", Position = "Senior Software Engineer", Email = "zack.w@pravaltech.com", Department = "IT" });
            _employees.Add(new Employee { Id = 5, Name = "Nester Wike", Position = "QA Specialist", Email = "nester.wi@pravaltech.com", Department = "Quality Assurance" });
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employees;
        }

        public void AddEmployee(Employee employee)
        {
            _employees.Add(employee);
        }
        public void DeleteEmployee(int employeeId)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == employeeId);
            if (employee != null)
            {
                _employees.Remove(employee);
            }
        }
        public void UpdateEmployee(Employee updatedEmployee)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == updatedEmployee.Id);
            if (employee != null)
            {
                employee.Name = updatedEmployee.Name;
                employee.Position = updatedEmployee.Position;
                employee.Department = updatedEmployee.Department;
                employee.Email = updatedEmployee.Email;
            }
        }

    }
}
