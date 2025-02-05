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
        private static readonly Random _random = new Random();
        public EmployeeService()
        {
            GenerateDummyEmployees(100);
            //_employees.Add(new Employee { Id = 1, Name = "Jim Carter", Position = "Software Engineer", Email = "jim.c@pravaltech.com", Department = "IT" });
            //_employees.Add(new Employee { Id = 2, Name = "Alex Murphy", Position = "Product Manager", Email = "alex.m@pravaltech.com", Department = "Buisness" });
            //_employees.Add(new Employee { Id = 3, Name = "Megan Bowen", Position = "Designer", Email = "megan.bown@pravaltech.com", Department = "Branding" });
            //_employees.Add(new Employee { Id = 4, Name = "Zack Nestor", Position = "Senior Software Engineer", Email = "zack.w@pravaltech.com", Department = "IT" });
            //_employees.Add(new Employee { Id = 5, Name = "Nester Wike", Position = "QA Specialist", Email = "nester.wi@pravaltech.com", Department = "Quality Assurance" });
        }

        private void GenerateDummyEmployees(int count)
        {
            string[] firstNames = { "Jim", "Alex", "Megan", "Zack", "Nester", "Olivia", "Emma", "Liam", "Noah", "Sophia" };
            string[] lastNames = { "Carter", "Murphy", "Bowen", "Nestor", "Wike", "Johnson", "Smith", "Brown", "Williams", "Davis" };
            string[] positions = { "Software Engineer", "Product Manager", "Designer", "Senior Software Engineer", "QA Specialist", "HR Manager", "Accountant", "Marketing Lead" };
            string[] departments = { "IT", "Business", "Branding", "Quality Assurance", "HR", "Finance", "Marketing" };

            for (int i = 1; i <= count; i++)
            {
                string firstName = firstNames[_random.Next(firstNames.Length)];
                string lastName = lastNames[_random.Next(lastNames.Length)];
                string fullName = $"{firstName} {lastName}";

                _employees.Add(new Employee
                {
                    Id = i,
                    Name = fullName,
                    Position = positions[_random.Next(positions.Length)],
                    Email = $"{firstName.ToLower()}.{lastName.ToLower()}@pravaltech.com",
                    Department = departments[_random.Next(departments.Length)]
                });
            }
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
