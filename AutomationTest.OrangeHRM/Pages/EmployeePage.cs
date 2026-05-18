using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OrangeHRM.AutomationTests.Config;

namespace OrangeHRM.AutomationTests.Pages
{
    public class EmployeePage : BasePage
    {
        private By AddEmployeeButton => By.CssSelector("button:has(.bi-plus)");
        private By FirstNameField => By.CssSelector("input.orangehrm-firstname");
        private By LastNameField => By.CssSelector("input.orangehrm-lastname");
        private By EmployeeIdField => By.XPath("//label[normalize-space()='Employee Id']/ancestor::div[contains(@class,'oxd-input-group')]//input");
        private By SaveButton => By.CssSelector("form:has(.orangehrm-firstname) button.orangehrm-left-space");
        private By SuccessMessage => By.CssSelector("#oxd-toaster_1 .oxd-toast--success");
        private By SearchButton => By.XPath("//button[contains(., 'Search')]");
        private By EditButton => By.CssSelector(".oxd-table-cell-action-space:has(.bi-pencil-fill)");
        private By DeleteButton => By.CssSelector(".oxd-table-cell-action-space:has(.bi-trash)");
        private By DeleteModal => By.CssSelector(".orangehrm-dialog-popup");
        private By ConfirmDeleteButton => By.CssSelector(".orangehrm-modal-footer button.oxd-button--label-danger");
        private By FormLoader => By.CssSelector(".oxd-form-loader");
        private By EmployeeRowById(string id) =>
            By.XPath($"//div[contains(@class,'oxd-table-row--with-border')]//*[normalize-space()='{id}']");

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
            WaitForElementToDisappear(FormLoader);
        }

        public void FillEmployee(string? firstName = null, string? lastName = null, string? employeeId = null)
        {
            if (firstName != null) ClearAndFill(FirstNameField, firstName);
            if (lastName != null) ClearAndFill(LastNameField, lastName);
            if (employeeId != null) ClearAndFill(EmployeeIdField, employeeId);
        }

        public bool SaveEmployee()
        {
            Click(SaveButton);
            return IsSuccessMessageDisplayed();
        }

        public bool IsSuccessMessageDisplayed()
        {
            try { WaitForElementVisible(SuccessMessage); return true; }
            catch { return false; }
        }

        public void SearchEmployeeById(string employeeId)
        {
            ClearAndFill(EmployeeIdField, employeeId);
            Click(SearchButton);
            WaitForElementVisible(EmployeeRowById(employeeId));
        }

        public void ClickEditEmployee()
        {
            Click(EditButton);
            WaitForElementToDisappear(FormLoader);
        }

        public bool DeleteEmployee()
        {
            Click(DeleteButton);
            WaitForElementVisible(DeleteModal);
            Click(ConfirmDeleteButton);
            WaitForElementToDisappear(DeleteModal);
            return IsSuccessMessageDisplayed();
        }

    }
}

