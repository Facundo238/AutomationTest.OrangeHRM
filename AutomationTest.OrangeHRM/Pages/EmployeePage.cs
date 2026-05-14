using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OrangeHRM.AutomationTests.Config;

namespace OrangeHRM.AutomationTests.Pages
{
    public class EmployeePage : BasePage
    {
        private By AddEmployeeButton => By.XPath("//a[contains(text(), 'Add')]");
        private By FirstNameField => By.XPath("//input[@placeholder='First Name']");
        private By LastNameField => By.XPath("//input[@placeholder='Last Name']");
        private By EmployeeIdField => By.XPath("//label[normalize-space()='Employee Id']/ancestor::div[contains(@class,'oxd-input-group')]//input");
        private By SaveButton => By.XPath("//button[@type='submit' and contains(., 'Save')]");
        private By SuccessMessage => By.XPath("//div[contains(@class,'oxd-toast--success')]");
        private By SearchButton => By.XPath("//button[contains(., 'Search')]");
        private By EditButton => By.XPath("//button[contains(@class,'oxd-table-cell-action-space')][.//i[contains(@class,'bi-pencil-fill')]]");
        private By DeleteButton => By.XPath("//button[contains(@class,'oxd-table-cell-action-space')][.//i[contains(@class,'bi-trash')]]");
        private By ConfirmDeleteButton => By.XPath("//button[contains(., 'Yes, Delete')]");
        private By EmployeeTableRow => By.XPath("//div[contains(@class,'oxd-table-row--with-border')]");

        public EmployeePage(IWebDriver driver) : base(driver)
        {
        }

        public void NavigateToEmployeeList()
        {
            Driver.Navigate().GoToUrl($"{ConfigurationManager.BaseUrl}web/index.php/pim/viewEmployeeList");
            WaitForElementVisible(AddEmployeeButton);
        }

        public void ClickAddEmployee()
        {
            Click(AddEmployeeButton);
            WaitForElementVisible(FirstNameField);
        }

        public void FillEmployeeData(string firstName, string lastName, string employeeId)
        {
            SendKeys(FirstNameField, firstName);
            SendKeys(LastNameField, lastName);
            var idInput = Driver.FindElement(EmployeeIdField);
            idInput.SendKeys(OpenQA.Selenium.Keys.Control + "a");
            idInput.SendKeys(OpenQA.Selenium.Keys.Delete);
            idInput.SendKeys(employeeId);
        }

        public void SaveEmployee()
        {
            Click(SaveButton);
            WaitForElementVisible(SuccessMessage);
        }

        public bool IsSuccessMessageDisplayed()
        {
            return IsElementPresent(SuccessMessage);
        }

        public void SearchEmployee(string employeeId)
        {
            SendKeys(EmployeeIdField, employeeId);
            Click(SearchButton);
            WaitForElementVisible(EmployeeTableRow);
        }

        public void EditEmployee(string firstName, string lastName)
        {
            Click(EditButton);

            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(EmployeeIdField);
                    return el.Displayed && el.GetAttribute("value")?.Length > 0;
                }
                catch (NoSuchElementException) { return false; }
            });

            var fnInput = WaitForElementVisible(FirstNameField);
            fnInput.SendKeys(OpenQA.Selenium.Keys.Control + "a");
            fnInput.SendKeys(OpenQA.Selenium.Keys.Delete);
            fnInput.SendKeys(firstName);

            var lnInput = WaitForElementVisible(LastNameField);
            lnInput.SendKeys(OpenQA.Selenium.Keys.Control + "a");
            lnInput.SendKeys(OpenQA.Selenium.Keys.Delete);
            lnInput.SendKeys(lastName);

            SaveEmployee();
        }

        public void DeleteEmployee()
        {
            Click(DeleteButton);
            Click(ConfirmDeleteButton);
            WaitForElementToDisappear(ConfirmDeleteButton);
        }

        public bool IsEmployeeDisplayedInList()
        {
            return IsElementPresent(EmployeeTableRow);
        }
    }
}
