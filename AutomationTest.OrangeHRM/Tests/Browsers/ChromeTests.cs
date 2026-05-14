using OrangeHRM.AutomationTests.Fixtures;
using Xunit.Abstractions;

namespace OrangeHRM.AutomationTests.Tests.Browsers
{
    [CollectionDefinition("Chrome")]
    public class ChromeCollection : ICollectionFixture<ChromeAuthFixture> { }

    [Collection("Chrome")] [Trait("Browser", "Chrome")] public class ChromeEmployeeTests(ChromeAuthFixture f, ITestOutputHelper o) : ABMEmployeeTest(f, o) { }
    [Collection("Chrome")] [Trait("Browser", "Chrome")] public class ChromeMyInfoTests(ChromeAuthFixture f) : MyInfoTest(f) { }
}
