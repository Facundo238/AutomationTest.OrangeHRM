using OrangeHRM.AutomationTests.Fixtures;
using Xunit.Abstractions;

namespace OrangeHRM.AutomationTests.Tests.Browsers
{
    [CollectionDefinition("Edge")]
    public class EdgeCollection : ICollectionFixture<EdgeAuthFixture> { }

    [Collection("Edge")] [Trait("Browser", "Edge")] public class EdgeEmployeeTests(EdgeAuthFixture f, ITestOutputHelper o) : ABMEmployeeTest(f, o) { }
    [Collection("Edge")] [Trait("Browser", "Edge")] public class EdgeMyInfoTests(EdgeAuthFixture f) : MyInfoTest(f) { }
}
