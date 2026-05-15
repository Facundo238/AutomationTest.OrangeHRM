using OpenQA.Selenium;
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
            WaitForElementVisible(SearchButton);
        }

        public void ClickAddEmployee()
        {
            Click(AddEmployeeButton);
            WaitForElementVisible(FirstNameField);
        }

        public void FillEmployee(string? firstName = null, string? lastName = null, string? employeeId = null)
        {
            if (firstName != null) ClearAndFill(FirstNameField, firstName);
            if (lastName != null) ClearAndFill(LastNameField, lastName);
            if (employeeId != null) ClearAndFill(EmployeeIdField, employeeId);
        }

        public void SaveEmployee()
        {
            Click(SaveButton);
        }

        public bool IsSuccessMessageDisplayed()
        {
            try { WaitForElementVisible(SuccessMessage); return true; }
            catch { return false; }
        }

        public void SearchEmployee(string employeeId)
        {
            SendKeys(EmployeeIdField, employeeId);
            Click(SearchButton);
            WaitForElementVisible(EmployeeTableRow);
        }

        public void ClickEditEmployee()
        {
            Click(EditButton);
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
