
using OrangeHRM.AutomationTests.Fixtures;
using OrangeHRM.AutomationTests.Helpers;

// Browser-specific runners
namespace OrangeHRM.AutomationTests.Tests
{
    [Trait("Browser", "Chrome")] public class ChromeMyInfoTests() : MyInfoTest(new ChromeAdminFixture()) { }
    [Trait("Browser", "Edge")]   public class EdgeMyInfoTests()   : MyInfoTest(new EdgeAdminFixture())   { }
}

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
