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
        private List<Employee> _allEmployees = new List<Employee>();
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
        private int _currentPage = 1;
        //private int _itemsPerPage = 10;
        private int _totalPages;

        public List<int> ItemsPerPageOptions { get; set; } = new List<int> { 5, 10, 20, 50 };

        private int _itemsPerPage;
        public int ItemsPerPage
        {
            get => _itemsPerPage;
            set
            {
                if (_itemsPerPage != value)
                {
                    _itemsPerPage = value;
                    NotifyOfPropertyChange(() => ItemsPerPage);
                    UpdatePagination();
                }
            }
        }

        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                NotifyOfPropertyChange(() => CurrentPage);
                NotifyOfPropertyChange(() => PageInfo);
                LoadEmployees();
            }
        }

        public int TotalPages
        {
            get => _totalPages;
            set
            {
                _totalPages = value;
                NotifyOfPropertyChange(() => TotalPages);
                NotifyOfPropertyChange(() => PageInfo);
            }
        }

        public string PageInfo => $"Page {CurrentPage} of {TotalPages}";

        public bool CanGoToFirstPage => CurrentPage > 1;
        public bool CanGoToPreviousPage => CurrentPage > 1;
        public bool CanGoToNextPage => CurrentPage < TotalPages;
        public bool CanGoToLastPage => CurrentPage < TotalPages;

        public void GoToFirstPage()
        {
            if (CanGoToFirstPage)
            {
                CurrentPage = 1;
                LoadEmployees();
                NotifyOfPropertyChange(() => CanGoToFirstPage);
                NotifyOfPropertyChange(() => CanGoToPreviousPage);
                NotifyOfPropertyChange(() => CanGoToNextPage);
                NotifyOfPropertyChange(() => CanGoToLastPage);
            }
        }

        public void GoToPreviousPage()
        {
            if (CanGoToPreviousPage)
            {
                CurrentPage--;
                LoadEmployees();
                NotifyOfPropertyChange(() => CanGoToFirstPage);
                NotifyOfPropertyChange(() => CanGoToPreviousPage);
                NotifyOfPropertyChange(() => CanGoToNextPage);
                NotifyOfPropertyChange(() => CanGoToLastPage);
            }
        }

        public void GoToNextPage()
        {
            if (CanGoToNextPage)
            {
                CurrentPage++;
                LoadEmployees();
                NotifyOfPropertyChange(() => CanGoToFirstPage);
                NotifyOfPropertyChange(() => CanGoToPreviousPage);
                NotifyOfPropertyChange(() => CanGoToNextPage);
                NotifyOfPropertyChange(() => CanGoToLastPage);
            }
        }

        public void GoToLastPage()
        {
            if (CanGoToLastPage)
            {
                CurrentPage = TotalPages;
                LoadEmployees();
                NotifyOfPropertyChange(() => CanGoToFirstPage);
                NotifyOfPropertyChange(() => CanGoToPreviousPage);
                NotifyOfPropertyChange(() => CanGoToNextPage);
                NotifyOfPropertyChange(() => CanGoToLastPage);
            }
        }

        public EmployeeViewModel(IWindowManager windowManager, EmployeeService employeeService)
        {
            _windowManager = windowManager;
            _employeeService = employeeService;
            Employees = new BindableCollection<Employee>();
            ItemsPerPage = 10;
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            _allEmployees = _employeeService.GetAllEmployees().ToList();
            UpdatePagination();
        }

        private void UpdatePagination()
        {
            if (ItemsPerPage <= 0) ItemsPerPage = 10;
            TotalPages = (int)Math.Ceiling((double)_allEmployees.Count / ItemsPerPage);
            CurrentPage = 1;
            LoadEmployees();
            NotifyOfPropertyChange(() => CanGoToFirstPage);
            NotifyOfPropertyChange(() => CanGoToPreviousPage);
            NotifyOfPropertyChange(() => CanGoToNextPage);
            NotifyOfPropertyChange(() => CanGoToLastPage);
        }

        private void LoadEmployees()
        {
            Employees.Clear();
            var pagedEmployees = _allEmployees
                .Skip((CurrentPage - 1) * ItemsPerPage)
                .Take(ItemsPerPage)
                .ToList();

            Employees.AddRange(pagedEmployees);
            NotifyOfPropertyChange(() => CanGoToFirstPage);
            NotifyOfPropertyChange(() => CanGoToPreviousPage);
            NotifyOfPropertyChange(() => CanGoToNextPage);
            NotifyOfPropertyChange(() => CanGoToLastPage);
            NotifyOfPropertyChange(() => PageInfo);
        }

        public void AddEmployee()
        {
            var addEmployeeViewModel = new AddEmployeeViewModel(_employeeService);
            _windowManager.ShowDialogAsync(addEmployeeViewModel);
            RefreshEmployees();
        }
        private void RefreshEmployees()
        {
            //var employeeList = _employeeService.GetAllEmployees();
            //Employees.Clear();
            //foreach (var employee in employeeList)
            //{
            //    Employees.Add(employee);
            //}
            _allEmployees = _employeeService.GetAllEmployees().ToList();
            UpdatePagination();
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
