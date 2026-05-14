using OpenQA.Selenium;
using OrangeHRM.AutomationTests.Config;

namespace OrangeHRM.AutomationTests.Pages
{
    public class LoginPage : BasePage
    {
        private By UsernameField => By.Name("username");
        private By PasswordField => By.Name("password");
        private By LoginButton => By.XPath("//button[@type='submit']");

        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        public void NavigateToLoginPage()
        {
            Driver.Navigate().GoToUrl(ConfigurationManager.BaseUrl);
        }

        public string GetToken() => GetSessionCookie();

        public void Login(string username, string password)
        {
            SendKeys(UsernameField, username);
            SendKeys(PasswordField, password);
            Click(LoginButton);
            WaitForUrlToNotContain("login");
        }

        public string GetSessionCookie()
        {
            var cookie = Driver.Manage().Cookies.GetCookieNamed("orangehrm");

            if (cookie == null)
                throw new Exception("Session cookie not found. Make sure you are logged in.");

            return cookie.Value;
        }
    }
}