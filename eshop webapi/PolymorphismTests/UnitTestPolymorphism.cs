using Microsoft.VisualStudio.TestTools.UnitTesting;
using Polymorphism.DataModel;

namespace PolymorphismTests
{
    [TestClass]
    public class UnitTestPolymorphism
    {


        [TestMethod]
        public void CalculateWeeklySalaryForEmployeeTest_70wage55hoursReturn2800DOllars() {
            //arrange
            int weeklyHours = 40;
            int wage = 70;
            int salary = 40 * wage; ;

            Employee employee = new Employee();
            string expectResponse = $"Angry employee worked {weeklyHours} hours. paid for 40 hrs at {wage}. hr = ${salary}";

            //act
            string response = employee.CalclateWeeklySalary(weeklyHours, wage);

            //asserts
            Assert.AreEqual(expectResponse, response);
        }

        [TestMethod]
        public void CalculateWeeklySalaryForContractorTest_70wage55hoursReturn2800DOllars()
        {
            //arrange
            int weeklyHours = 55;
            int wage = 70;
            int salary = 55 * 70;

            Contractor contractor = new Contractor();
            string expectResponse = $"Happy Contractor worked {weeklyHours} hours. paid for {weeklyHours} hrs at {wage} hr = ${salary}";

            //act
            string response = contractor.CalclateWeeklySalary(weeklyHours, wage);

            //asserts
            Assert.AreEqual(expectResponse, response);
        }

        [TestMethod]
        public void CalculateWeeklySalaryForEmployeeTest_70wage55hoursDoseNotReturnCorrectString() {
            //arrange
            int weeklyHours = 40;
            int wage = 70;
            int salary = 40 * wage;

            Employee employee = new Employee();
            string expectResponse = "Problem 1 " + $"Angry employee worked {weeklyHours} hours. paid for 40 hrs at {wage}. hr = ${salary}";

            //act
            string response = employee.CalclateWeeklySalary(weeklyHours, wage);

            //asserts
            Assert.AreNotEqual(expectResponse, response);
        }
    }
}