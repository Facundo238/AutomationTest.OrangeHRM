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
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(ElementClickInterceptedException));
            wait.Until(d =>
            {
                WaitForElementVisible(locator).Click();
                return true;
            });
        }

        protected void SendKeys(By locator, string text)
        {
            WaitForElementVisible(locator).SendKeys(text);
        }

        protected void ClearAndFill(By locator, string text)
        {
            var el = WaitForElementVisible(locator);
            el.SendKeys(OpenQA.Selenium.Keys.Control + "a");
            el.SendKeys(OpenQA.Selenium.Keys.Delete);
            el.SendKeys(text);
        }

        protected void WaitForElementToHaveValue(By locator, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));
            wait.Until(d =>
            {
                var el = d.FindElement(locator);
                return el.Displayed && el.GetAttribute("value")?.Length > 0;
            });
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
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));
            return wait.Until(d =>
            {
                var el = d.FindElement(locator);
                return el.Displayed ? el : null;
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
