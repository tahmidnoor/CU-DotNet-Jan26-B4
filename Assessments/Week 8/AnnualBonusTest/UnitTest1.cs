using PerformanceBonus;

namespace AnnualBonusTest
{
    public class Tests
    {
        [Test]
        public void NormalHighPerformer()
        {
            var emp = new EmployeeBonus
            {
                BaseSalary = 500000m,
                PerformanceRating = 5,
                YearsOfExperience = 6,
                DepartmentMultiplier = 1.1m,
                AttendancePercentage = 95
            };
            Assert.That(emp.NetAnnualBonus, Is.EqualTo(123200.00));
        }

        [Test]
        public void AttendancePenaltyApplied()
        {
            var emp = new EmployeeBonus
            {
                BaseSalary = 400000m,
                PerformanceRating = 4,
                YearsOfExperience = 8,
                DepartmentMultiplier = 1.0m,
                AttendancePercentage = 80
            };
            Assert.That(emp.NetAnnualBonus, Is.EqualTo(60480.00));
        }

        [Test]
        public void CapTriggered()
        {
            var emp = new EmployeeBonus
            {
                BaseSalary = 1000000m,
                PerformanceRating = 5,
                YearsOfExperience = 15,
                DepartmentMultiplier = 1.5m,
                AttendancePercentage = 95
            };
            Assert.That(emp.NetAnnualBonus, Is.EqualTo(280000.00));
        }

        [Test]
        public void LowPerformer()
        {
            var emp = new EmployeeBonus
            {
                BaseSalary = 300000m,
                PerformanceRating = 2,
                YearsOfExperience = 3,
                DepartmentMultiplier = 1.0m,
                AttendancePercentage = 90
            };
            Assert.That(emp.NetAnnualBonus, Is.EqualTo(13500.00));
        }

        [Test]
        public void TaxBoundary()
        {
            var emp = new EmployeeBonus
            {
                BaseSalary = 600000m,
                PerformanceRating = 3,
                YearsOfExperience = 0,
                DepartmentMultiplier = 1.0m,
                AttendancePercentage = 100
            };
            Assert.That(emp.NetAnnualBonus, Is.EqualTo(64800.00));
        }

        [Test]
        public void HighTaxSlab()
        {
            var emp = new EmployeeBonus
            {
                BaseSalary = 900000m,
                PerformanceRating = 5,
                YearsOfExperience = 11,
                DepartmentMultiplier = 1.2m,
                AttendancePercentage = 100
            };
            Assert.That(emp.NetAnnualBonus, Is.EqualTo(226800.00));
        }

        [Test]
        public void RoundingPrecision()
        {
            var emp = new EmployeeBonus
            {
                BaseSalary = 555555m,
                PerformanceRating = 4,
                YearsOfExperience = 6,
                DepartmentMultiplier = 1.13m,
                AttendancePercentage = 92
            };
            Assert.That(emp.NetAnnualBonus, Is.EqualTo(118649.88));
        }
    }
}