namespace OrangeHRM.AutomationTests.Helpers
{
    public record EmployeeTestData(string FirstName, string LastName, string EmployeeId);

    public static class EmployeeDataBuilder
    {
        public static EmployeeTestData NewEmployee(string prefix = "Test")
        {
            var uid = Guid.NewGuid().ToString("N")[..6];
            var employeeId = $"E{uid}";
            return new($"{prefix}_{uid}", $"Auto_{uid}", employeeId);
        }
    }
}
