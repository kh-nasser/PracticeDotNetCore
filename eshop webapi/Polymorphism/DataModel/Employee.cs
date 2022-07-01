using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polymorphism.DataModel
{
    public class Employee
    {
        public virtual string CalclateWeeklySalary(int weeklyHours, int wage)
        {
            var salary = 40 * wage;

            string result = $"Angry employee worked {weeklyHours} hours. paid for 40 hrs at {wage}. hr = ${salary}";
            Console.WriteLine(result);
            Console.WriteLine($"--------------------------{Environment.NewLine}");
            return result;

        }
    }
}
