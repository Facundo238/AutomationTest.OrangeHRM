using OrangeHRM.AutomationTests.Fixtures;

namespace OrangeHRM.AutomationTests.Tests
{
    [CollectionDefinition("Chrome-Admin")]
    public class ChromeAdminCollection : ICollectionFixture<ChromeAdminFixture> { }

    [CollectionDefinition("Edge-Admin")]
    public class EdgeAdminCollection : ICollectionFixture<EdgeAdminFixture> { }
}
