using OpenQA.Selenium;
using OrangeHRM.AutomationTests.Config;
using OrangeHRM.AutomationTests.Pages;

namespace OrangeHRM.AutomationTests.Fixtures
{
    public abstract class AuthFixture : IDisposable
    {
        public IWebDriver Driver { get; }
        public string Token { get; }
        public string Browser { get; }

        protected AuthFixture(string browser, string account)
        {
            Browser = browser;
            var (username, password) = ConfigurationManager.GetUser(account);
            Driver = WebDriverFactory.CreateDriver(browser);

            var loginPage = new LoginPage(Driver);
            loginPage.NavigateToLoginPage();
            loginPage.Login(username, password);

            Token = loginPage.GetSessionCookie();
        }

        public void Dispose()
        {
            Driver.Quit();
            Driver.Dispose();
        }
    }

    public abstract class BrowserFixture(string browser, string role) : AuthFixture(browser, role);

    public class ChromeAdminFixture() : BrowserFixture("chrome", "Admin");
    public class EdgeAdminFixture()   : BrowserFixture("edge",   "Admin");
}
