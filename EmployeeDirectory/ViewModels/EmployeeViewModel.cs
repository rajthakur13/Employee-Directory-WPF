using EmployeeDirectory.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;

public class EmployeeViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Employee> Employees { get; set; }

    private Employee? _selectedEmployee;

    public Employee? SelectedEmployee
    {
        get => _selectedEmployee;
        set
        {
            if (_selectedEmployee != value)
            {
                _selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public EmployeeViewModel()
    {
        Employees = new ObservableCollection<Employee>();
    }

    public void AddEmployee(Employee employee)
    {
        employee.Id = Employees.Count + 1;
        Employees.Add(employee);
    }

    public void UpdateEmployee(Employee employee)
    {
        var existingEmployee = Employees.FirstOrDefault(e => e.Id == employee.Id);
        if (existingEmployee != null)
        {
            existingEmployee.Name = employee.Name;
            existingEmployee.Position = employee.Position;
            existingEmployee.Department = employee.Department;
            existingEmployee.Email = employee.Email;
        }
    }

    public void DeleteEmployee(Employee employee)
    {
        Employees.Remove(employee);
    }
}
