using OrangeHRM.AutomationTests.Fixtures;

namespace OrangeHRM.AutomationTests.Tests
{
    [CollectionDefinition("ChromeABMEmployee")]
    public class ChromeCollection : ICollectionFixture<ChromeAuthFixture> { }

    [CollectionDefinition("EdgeABMEmployee")]
    public class EdgeCollection : ICollectionFixture<EdgeAuthFixture> { }
}
