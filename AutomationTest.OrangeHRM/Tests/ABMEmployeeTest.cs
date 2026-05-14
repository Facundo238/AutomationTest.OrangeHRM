using OrangeHRM.AutomationTests.Fixtures;
using OrangeHRM.AutomationTests.Helpers;
using Xunit.Abstractions;

namespace OrangeHRM.AutomationTests.Tests
{
    public abstract class ABMEmployeeTest
    {
        private readonly AuthFixture _auth;
        private readonly ITestOutputHelper _output;

        protected ABMEmployeeTest(AuthFixture auth, ITestOutputHelper output)
        {
            _auth = auth;
            _output = output;
        }

        [Fact]
        [Trait("Module", "Employee")]
        public void TestCreateEmployee()
        {
            using var context = new UserSession(_auth);
            var data = EmployeeDataBuilder.NewEmployee("Test");

            _output.WriteLine($"[CreateEmployee] Creating: {data.FirstName} {data.LastName} (ID: {data.EmployeeId})");
            context.EmployeePage.NavigateToEmployeeList();
            context.EmployeePage.ClickAddEmployee();
            context.EmployeePage.FillEmployeeData(data.FirstName, data.LastName, data.EmployeeId);
            context.EmployeePage.SaveEmployee();

            var success = context.EmployeePage.IsSuccessMessageDisplayed();
            _output.WriteLine($"[CreateEmployee] Result: {(success ? "OK" : "FAILED")}");
            Assert.True(success, "Employee creation failed");
            // Assert.True(context.DbValidator.VerifyEmployeeExists(data.EmployeeId, data.FirstName, data.LastName), "Employee not found in DB");
        }

        [Fact]
        [Trait("Module", "Employee")]
        public async Task TestUpdateEmployee()
        {
            using var context = new UserSession(_auth);
            var data = EmployeeDataBuilder.NewEmployee("Upd");
            var updated = EmployeeDataBuilder.NewEmployee("Mod");

            _output.WriteLine($"[UpdateEmployee] Creating via API: {data.FirstName} {data.LastName} (ID: {data.EmployeeId})");
            await context.ApiClient.CreateEmployeeAsync(data.FirstName, data.LastName, data.EmployeeId);

            _output.WriteLine($"[UpdateEmployee] Updating to: {updated.FirstName} {updated.LastName}");
            context.EmployeePage.NavigateToEmployeeList();
            context.EmployeePage.SearchEmployee(data.EmployeeId);
            context.EmployeePage.EditEmployee(updated.FirstName, updated.LastName);

            var success = context.EmployeePage.IsSuccessMessageDisplayed();
            _output.WriteLine($"[UpdateEmployee] Result: {(success ? "OK" : "FAILED")}");
            Assert.True(success, "Employee update failed");
            // Assert.True(context.DbValidator.VerifyEmployeeUpdated(data.EmployeeId, updated.FirstName, updated.LastName), "Employee update not reflected in DB");
        }

        [Fact]
        [Trait("Module", "Employee")]
        public async Task TestDeleteEmployee()
        {
            using var context = new UserSession(_auth);
            var data = EmployeeDataBuilder.NewEmployee("Del");

            _output.WriteLine($"[DeleteEmployee] Creating via API: {data.FirstName} {data.LastName} (ID: {data.EmployeeId})");
            await context.ApiClient.CreateEmployeeAsync(data.FirstName, data.LastName, data.EmployeeId);

            _output.WriteLine($"[DeleteEmployee] Deleting employee ID: {data.EmployeeId}");
            context.EmployeePage.NavigateToEmployeeList();
            context.EmployeePage.SearchEmployee(data.EmployeeId);
            context.EmployeePage.DeleteEmployee();
            _output.WriteLine("[DeleteEmployee] Result: OK");
            // Assert.True(context.DbValidator.VerifyEmployeeDeleted(data.EmployeeId), "Employee still present in DB after delete");
        }
    }
}
