using System;
using System.Linq;
namespace CompanyManagement
{
    public abstract class Employee
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }
        protected Employee(string name, int age, decimal salary)
        {
            Name = name;
            Age = age;
            Salary = salary;
        }
    }
    public sealed class Manager : Employee
    {
        public Manager(string name, int age, decimal salary) : base(name, age, salary) { }
    }
    public sealed class Developer : Employee
    {
        public Developer(string name, int age, decimal salary) : base(name, age, salary) { }
    }
    public class Company
    {
        public Employee[] Employees { get; set; }
        public Company(Employee[] employees)
        {
            Employees = employees;
        }
        public Employee GetHighestPaidEmployee()
        {
            if (Employees == null || Employees.Length == 0) return null;
            return Employees.OrderByDescending(e => e.Salary).First();
        }
        public double GetAverageAge()
        {
            if (Employees == null || Employees.Length == 0) return 0;
            return Employees.Average(e => e.Age);
        }
    }
    class Program
    {
        static void Main()
        {
            Employee[] staff = new Employee[]
            {
                new Manager("Алексей", 45, 150000),
                new Developer("Дмитрий", 28, 120000),
                new Developer("Мария", 32, 135000),
                new Manager("Ирина", 38, 140000)
            };
            Company myCompany = new Company(staff);
            var richEmployee = myCompany.GetHighestPaidEmployee();
            double avgAge = myCompany.GetAverageAge();
            Console.WriteLine($"Самая высокая зарплата у: {richEmployee?.Name} ({richEmployee?.Salary})");
            Console.WriteLine($"Средний возраст сотрудников: {avgAge:F1} лет");
        }
    }
}