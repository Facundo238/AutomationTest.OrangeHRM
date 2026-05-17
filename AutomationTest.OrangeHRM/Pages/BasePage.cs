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
            Click(locator);
            SendKeys(locator, Keys.Control + "a");
            SendKeys(locator, text);
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
