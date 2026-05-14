using OpenQA.Selenium;
using OrangeHRM.AutomationTests.Config;

namespace OrangeHRM.AutomationTests.Pages
{
    public class MyInfoPage : BasePage
    {
        private By PersonalDetailsTitle => By.XPath("//h6[contains(@class,'orangehrm-main-title') and normalize-space()='Personal Details']");
        private By MyInfoMenuItem => By.XPath("//a[contains(@class,'oxd-main-menu-item') and .//span[normalize-space()='My Info']]");

        public MyInfoPage(IWebDriver driver) : base(driver) { }

        public void NavigateToMyInfo()
        {
            Driver.Navigate().GoToUrl(ConfigurationManager.BaseUrl);
            Click(MyInfoMenuItem);
            WaitForElementVisible(PersonalDetailsTitle);
        }

        public bool IsPersonalDetailsTitleVisible()
        {
            return IsElementPresent(PersonalDetailsTitle);
        }
    }
}
