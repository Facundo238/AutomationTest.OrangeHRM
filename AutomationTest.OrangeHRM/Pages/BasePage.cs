using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace OrangeHRM.AutomationTests.Pages
{
    public class BasePage : IDisposable
    {
        protected readonly IWebDriver Driver;

        public IWebDriver WebDriver => Driver;

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
        }

        protected void Click(By locator)
        {
            WaitForElementVisible(locator).Click();
        }

        protected void SendKeys(By locator, string text)
        {
            WaitForElementVisible(locator).SendKeys(text);
        }

        protected string GetText(By locator)
        {
            return WaitForElementVisible(locator).Text;
        }

        protected void WaitForElementAndClick(By locator, int timeoutSeconds = 10)
        {
            Click(locator);
        }

        protected IWebElement WaitForElementVisible(By locator, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));
            return wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(locator);
                    return el.Displayed ? el : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            })!;
        }

        protected void WaitForElementToDisappear(By locator, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));
            wait.Until(d =>
            {
                try { return !d.FindElement(locator).Displayed; }
                catch (NoSuchElementException) { return true; }
            });
        }

        protected void WaitForUrlToNotContain(string urlPart, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));
            wait.Until(d => !d.Url.Contains(urlPart));
        }

        protected bool IsElementPresent(By locator)
        {
            try
            {
                Driver.FindElement(locator);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public virtual void Dispose()
        {
            Driver?.Quit();
            Driver?.Dispose();
        }
    }
}
