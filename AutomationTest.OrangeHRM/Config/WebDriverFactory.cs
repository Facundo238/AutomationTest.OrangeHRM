using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace OrangeHRM.AutomationTests.Config
{
    public static class WebDriverFactory
    {
        public static IWebDriver CreateDriver(string browserName)
        {
            return browserName.ToLower() switch
            {
                "chrome" => CreateChromeDriver(),
                "edge" => CreateEdgeDriver(),
                _ => throw new ArgumentException($"Browser {browserName} not supported")
            };
        }

        private static IWebDriver CreateChromeDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            var driver = new ChromeDriver(options);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            return driver;
        }

        private static IWebDriver CreateEdgeDriver()
        {
            new DriverManager().SetUpDriver(new EdgeConfig());
            var options = new EdgeOptions();
            options.AddArgument("--start-maximized");
            var driver = new EdgeDriver(options);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            return driver;
        }
    }
}
