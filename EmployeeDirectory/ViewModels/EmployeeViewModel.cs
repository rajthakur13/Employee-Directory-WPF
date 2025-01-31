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
    public class EmployeeViewModel : Screen
    {
        private readonly IWindowManager _windowManager;
        private readonly EmployeeService _employeeService;
        private Employee _selectedEmployee;
        public BindableCollection<Employee> Employees { get; set; }
        public Employee SelectedEmployee
        {
            get
            {
                return _selectedEmployee;
            }

            set
            {
                _selectedEmployee = value;
                NotifyOfPropertyChange(() => SelectedEmployee);

            }
        }
        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            Employees.Add(new Employee { Id = 1, Name = "Jim Carter", Position = "Software Engineer", Email = "jim.c@pravaltech.com", Department = "IT" });
            Employees.Add(new Employee { Id = 2, Name = "Alex Murphy", Position = "Product Manager", Email = "alex.m@pravaltech.com", Department = "Buisness" });
            Employees.Add(new Employee { Id = 3, Name = "Megan Bowen", Position = "Designer", Email = "megan.bown@pravaltech.com", Department = "Branding" });
            Employees.Add(new Employee { Id = 4, Name = "Zack Nestor", Position = "Senior Software Engineer", Email = "zack.w@pravaltech.com", Department = "IT" });
            Employees.Add(new Employee { Id = 5, Name = "Nester Wike", Position = "QA Specialist", Email = "nester.wi@pravaltech.com", Department = "Quality Assurance" });
        }
        public EmployeeViewModel(IWindowManager windowManager, EmployeeService employeeService)
        {
            _windowManager = windowManager;
            _employeeService = employeeService;
            Employees = new BindableCollection<Employee>();
        }

        public void AddEmployee()
        {
            var addEmployeeViewModel = new AddEmployeeViewModel(_employeeService);
            _windowManager.ShowDialogAsync(addEmployeeViewModel);
            RefreshEmployees();
        }
        private void RefreshEmployees()
        {
            var employeeList = _employeeService.GetAllEmployees();
            Employees.Clear();
            foreach (var employee in employeeList)
            {
                Employees.Add(employee);
            }
        }
        public void DeleteEmployee()
        {
            _employeeService.DeleteEmployee(SelectedEmployee.Id);
            Employees.Remove(SelectedEmployee);
            RefreshEmployees();
        }

        public void EditEmployee()
        {
            //if (selectedEmployee == null) return;

            var addEmployeeViewModel = new AddEmployeeViewModel(_employeeService, SelectedEmployee);
            _windowManager.ShowDialogAsync(addEmployeeViewModel);
            RefreshEmployees();
        }
    }
}
