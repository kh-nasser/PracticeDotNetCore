using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polymorphism.DataModel
{
    public class Contractor : Employee
    {
        public override string CalclateWeeklySalary(int weeklyHours, int wage)
        {
            var salary = weeklyHours * wage;

            var result = $"Happy Contractor worked {weeklyHours} hours. paid for {weeklyHours} hrs at {wage} hr = ${salary}";
            Console.WriteLine(result);

            Console.WriteLine($"--------------------------{Environment.NewLine}");

            return result;
        }
    }
}
