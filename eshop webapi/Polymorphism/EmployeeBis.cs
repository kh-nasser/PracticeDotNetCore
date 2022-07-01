using Polymorphism.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polymorphism
{
    public class EmployeeBis
    {
        //public List<Employee> GetEmployees() => new List<Employee> { new Employee(), new Contractor() };
        public virtual List<Employee> GetMockEmployees() => throw new NotImplementedException();
    }
}
