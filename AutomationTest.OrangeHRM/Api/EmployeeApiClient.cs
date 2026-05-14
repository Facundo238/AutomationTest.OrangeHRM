using System.Text;
using System.Text.Json;
using OrangeHRM.AutomationTests.Config;

namespace OrangeHRM.AutomationTests.Api
{
    public class EmployeeApiClient : IDisposable
    {
        private readonly HttpClient _httpClient;

        public EmployeeApiClient(string sessionCookie)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(ConfigurationManager.BaseUrl);
            _httpClient.DefaultRequestHeaders.Add("Cookie", $"orangehrm={sessionCookie}");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<string> CreateEmployeeAsync(string firstName, string lastName, string? employeeId = null, string? baseUrl = null)
        {
            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };
            var body = JsonSerializer.Serialize(new { firstName, lastName, employeeId }, options);

            var content = new StringContent(body, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("web/index.php/api/v2/pim/employees", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseContent);

            return doc.RootElement
                .GetProperty("data")
                .GetProperty("empNumber")
                .GetInt32()
                .ToString();
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}