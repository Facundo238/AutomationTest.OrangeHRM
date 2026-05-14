using OrangeHRM.AutomationTests.Fixtures;
using Xunit.Abstractions;

namespace OrangeHRM.AutomationTests.Tests
{
    [CollectionDefinition("Chrome")]
    public class ChromeCollection : ICollectionFixture<ChromeAuthFixture> { }

    [CollectionDefinition("Edge")]
    public class EdgeCollection : ICollectionFixture<EdgeAuthFixture> { }

    [Collection("Chrome")] [Trait("Browser", "Chrome")] public class ChromeEmployeeTests(ChromeAuthFixture f, ITestOutputHelper o) : ABMEmployeeTest(f, o) { }
    [Collection("Chrome")] [Trait("Browser", "Chrome")] public class ChromeMyInfoTests(ChromeAuthFixture f) : MyInfoTest(f) { }

    [Collection("Edge")] [Trait("Browser", "Edge")] public class EdgeEmployeeTests(EdgeAuthFixture f, ITestOutputHelper o) : ABMEmployeeTest(f, o) { }
    [Collection("Edge")] [Trait("Browser", "Edge")] public class EdgeMyInfoTests(EdgeAuthFixture f) : MyInfoTest(f) { }
}
