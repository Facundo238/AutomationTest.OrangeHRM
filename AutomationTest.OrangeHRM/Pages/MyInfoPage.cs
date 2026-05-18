using OpenQA.Selenium;
using OrangeHRM.AutomationTests.Config;

namespace OrangeHRM.AutomationTests.Pages
{
    public class MyInfoPage : BasePage
    {
        private By PersonalDetailsTitle => By.CssSelector("h6.orangehrm-main-title");
        private By MyInfoMenuItem       => By.CssSelector("a[href*='viewMyDetails']");

        public MyInfoPage(IWebDriver driver) : base(driver) { }

        public void NavigateToMyInfo()
        {
            Click(MyInfoMenuItem);
            WaitForElementVisible(PersonalDetailsTitle);
        }

        public bool IsPersonalDetailsTitleVisible()
        {
            return IsElementPresent(PersonalDetailsTitle);
        }
    }
}
