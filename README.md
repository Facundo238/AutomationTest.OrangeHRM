# AutomationTest.OrangeHRM

Selenium test automation suite for [OrangeHRM](https://opensource-demo.orangehrmlive.com/) covering Employee ABM and My Info modules, running on Chrome and Edge in parallel.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Google Chrome (latest)
- Microsoft Edge (latest)
- OrangeHRM 

## Configuration

Create `AutomationTest.OrangeHRM/appsettings.json` (not committed — contains credentials):

```json
{
  "OrangeHRM": {
    "BaseUrl": "",
    "Users": {
      "Admin": {
        "Username": "",
        "Password": ""
      }
    }
  },
  "ConnectionStrings": {
    "OrangeHRM": ""
  }
}
```

## Running Tests

Run all tests:
```bash
dotnet test
```

Filter by browser:
```bash
dotnet test --filter "Browser=Chrome"
dotnet test --filter "Browser=Edge"
```

Filter by module:
```bash
dotnet test --filter "Module=Employee"
dotnet test --filter "Module=MyInfo"
```

Combined filter:
```bash
dotnet test --filter "Browser=Chrome&Module=Employee"
```

## Project Structure

```
AutomationTest.OrangeHRM/
├── Config/
│   ├── ConfigurationManager.cs   # Reads appsettings.json
│   └── WebDriverFactory.cs       # Creates Chrome/Edge drivers
├── Fixtures/
│   └── AuthFixture.cs            # Shared login fixture per browser
├── Helpers/
│   ├── EmployeeDataBuilder.cs    # Generates test employee data
│   ├── EmployeeDbValidator.cs    # DB assertions
│   └── UserSession.cs            # Scoped page object container
├── Pages/
│   ├── BasePage.cs               # Common Selenium helpers
│   ├── EmployeePage.cs           # Employee list/form interactions
│   ├── LoginPage.cs              # Login flow
│   └── MyInfoPage.cs             # My Info module interactions
├── Tests/
│   ├── ABMEmployeeTest.cs        # Employee Create/Update/Delete test logic
│   ├── BrowserTests.cs           # Wires Chrome and Edge collections
│   └── MyInfoTest.cs             # My Info test logic
└── appsettings.json              # Local config (not committed)
```

## Architecture

Tests are split into **base classes** (logic) and **browser wrappers** (infrastructure):

- `ABMEmployeeTest` and `MyInfoTest` contain the actual test logic
- `BrowserTests.cs` wires concrete Chrome/Edge subclasses to xUnit collections and fixtures
- Employee tests use `ICollectionFixture` — one shared `AuthFixture` (single browser) per suite, tests run sequentially within the collection
- MyInfo tests create their own `AuthFixture` instance per run — isolated browser, no shared state

## Parallelism

Configured in `xunit.runner.json`:
```json
{
  "parallelizeAssembly": true,
  "parallelizeTestCollections": true,
  "maxParallelThreads": 2
}
```

Collections run in parallel up to `maxParallelThreads`. Chrome and Edge Employee suites each form a collection (`ChromeABMEmployee` / `EdgeABMEmployee`). MyInfo tests have no collection — each gets its own implicit collection and opens a fresh browser.
