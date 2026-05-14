using OrangeHRM.AutomationTests.Fixtures;
using OrangeHRM.AutomationTests.Helpers;

namespace OrangeHRM.AutomationTests.Tests
{
    public abstract class MyInfoTest
    {
        private readonly AuthFixture _auth;

        protected MyInfoTest(AuthFixture auth)
        {
            _auth = auth;
        }

        [Fact]
        [Trait("Module", "MyInfo")]
        public void TestNavigateToMyInfo()
        {
            using var context = new TestContext(_auth);
            //using var context2 = new TestContext(_auth, "Admin2");

            context.MyInfoPage.NavigateToMyInfo();
            Assert.True(context.MyInfoPage.IsPersonalDetailsTitleVisible(), "Personal Details not visible for Admin");

            //context2.MyInfoPage.NavigateToMyInfo();
            //Assert.True(context2.MyInfoPage.IsPersonalDetailsTitleVisible(), "Personal Details not visible for Admin2");
        }
    }
}
