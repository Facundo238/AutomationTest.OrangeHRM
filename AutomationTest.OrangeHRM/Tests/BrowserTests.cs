using OrangeHRM.AutomationTests.Fixtures;
using Xunit.Abstractions;

namespace OrangeHRM.AutomationTests.Tests
{
    [CollectionDefinition("ChromeABMEmployee")]
    public class ChromeCollection : ICollectionFixture<ChromeAuthFixture> { }

    [CollectionDefinition("EdgeABMEmployee")]
    public class EdgeCollection : ICollectionFixture<EdgeAuthFixture> { }

    [Collection("ChromeABMEmployee")] [Trait("Browser", "Chrome")] public class ChromeEmployeeTests(ChromeAuthFixture f, ITestOutputHelper o) : ABMEmployeeTest(f, o) { }
    [Trait("Browser", "Chrome")] public class ChromeMyInfoTests() : MyInfoTest(new ChromeAuthFixture()) { }

    [Collection("EdgeABMEmployee")] [Trait("Browser", "Edge")] public class EdgeEmployeeTests(EdgeAuthFixture f, ITestOutputHelper o) : ABMEmployeeTest(f, o) { }
    [Trait("Browser", "Edge")] public class EdgeMyInfoTests() : MyInfoTest(new EdgeAuthFixture()) { }
}
