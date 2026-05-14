using MySqlConnector;

namespace OrangeHRM.AutomationTests.Helpers
{
    public class EmployeeDbValidator
    {
        private readonly string _connectionString;

        public EmployeeDbValidator(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool VerifyEmployeeExists(string employeeId, string firstName, string lastName)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            using var cmd = new MySqlCommand(
                "SELECT COUNT(*) FROM hs_hr_employee WHERE employee_id = @id AND emp_firstname = @fn AND emp_lastname = @ln",
                conn);
            cmd.Parameters.AddWithValue("@id", employeeId);
            cmd.Parameters.AddWithValue("@fn", firstName);
            cmd.Parameters.AddWithValue("@ln", lastName);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public bool VerifyEmployeeUpdated(string employeeId, string firstName, string lastName)
        {
            return VerifyEmployeeExists(employeeId, firstName, lastName);
        }

        public bool VerifyEmployeeDeleted(string employeeId)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            using var cmd = new MySqlCommand(
                "SELECT COUNT(*) FROM hs_hr_employee WHERE employee_id = @id",
                conn);
            cmd.Parameters.AddWithValue("@id", employeeId);
            return Convert.ToInt32(cmd.ExecuteScalar()) == 0;
        }
    }
}
