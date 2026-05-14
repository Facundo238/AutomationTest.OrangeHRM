using OpenQA.Selenium;
using OrangeHRM.AutomationTests.Api;
using OrangeHRM.AutomationTests.Config;
using OrangeHRM.AutomationTests.Fixtures;
using OrangeHRM.AutomationTests.Pages;

namespace OrangeHRM.AutomationTests.Helpers
{
    public sealed class UserSession : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly string _token;
        private readonly bool _ownsDriver;
        private EmployeePage? _employeePage;
        private MyInfoPage? _profilePage;
        private EmployeeApiClient? _apiClient;
        private EmployeeDbValidator? _dbValidator;

        public EmployeePage EmployeePage => _employeePage ??= new EmployeePage(_driver);
        public MyInfoPage MyInfoPage => _profilePage ??= new MyInfoPage(_driver);
        public EmployeeApiClient ApiClient => _apiClient ??= new EmployeeApiClient(_token);
        public EmployeeDbValidator DbValidator => _dbValidator ??= new EmployeeDbValidator(ConfigurationManager.DbConnectionString);

        public UserSession(AuthFixture auth, string? account = null)
        {
            if (account is null)
            {
                _driver = auth.Driver;
                _token = auth.Token;
                _ownsDriver = false;
            }
            else
            {
                var (username, password) = ConfigurationManager.GetUser(account);
                _driver = WebDriverFactory.CreateDriver(auth.Browser);
                _ownsDriver = true;

                var loginPage = new LoginPage(_driver);
                loginPage.NavigateToLoginPage();
                loginPage.Login(username, password);
                _token = loginPage.GetSessionCookie();
            }
        }

        public void Dispose()
        {
            _apiClient?.Dispose();
            if (_ownsDriver)
            {
                _driver.Quit();
                _driver.Dispose();
            }
        }
    }
}
