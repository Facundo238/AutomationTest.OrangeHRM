using OrangeHRM.AutomationTests.Fixtures;
using OrangeHRM.AutomationTests.Helpers;

namespace OrangeHRM.AutomationTests.Tests
{
    public abstract class MyInfoTest(AuthFixture auth) : IDisposable
    {
        private readonly AuthFixture _auth = auth;

        [Fact]
        [Trait("Module", "MyInfo")]
        public void TestNavigateToMyInfo()
        {
            using var context = new UserSession(_auth);
            context.MyInfoPage.NavigateToMyInfo();
            Assert.True(context.MyInfoPage.IsPersonalDetailsTitleVisible(), "Personal Details not visible for Admin");
        }

        public void Dispose() => _auth.Dispose();
    }
}
