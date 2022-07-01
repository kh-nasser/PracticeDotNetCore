// See https://aka.ms/new-console-template for more information

using Moq;
using Polymorphism;
using Polymorphism.DataModel;

//string name = Environment.GetCommandLineArgs()[1];
Console.WriteLine($"Hello, World! ");
const int hour = 55, wage = 70;

var mock = new Mock<EmployeeBis>();
mock.Setup(m => m.GetMockEmployees()).Returns(() => 
new List<Employee>
{
    new Employee(), 
    new Contractor()
});
//List<Employee> employees = new EmployeeBis().GetEmployees();
List<Employee> employees = mock.Object.GetMockEmployees();

foreach (var employee in employees)
{
    employee.CalclateWeeklySalary(hour, wage);
}