# AutomationTest.OrangeHRM

Selenium test automation suite for [OrangeHRM](https://opensource-demo.orangehrmlive.com/) covering Employee ABM and My Info modules, running on Chrome and Edge in parallel.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Google Chrome (latest)
- Microsoft Edge (latest)
- OrangeHRM instance running and accessible

## Configuration

Create `AutomationTest.OrangeHRM/appsettings.json` (not committed — contains credentials):

```json
{
  "OrangeHRM": {
    "BaseUrl": "https://your-orangehrm-instance/",
    "Users": {
      "Admin": {
        "Username": "admin",
        "Password": "admin123"
      }
    }
  },
  "ConnectionStrings": {
    "OrangeHRM": "Server=localhost;Database=orangehrm;User=root;Password=secret;"
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

- `ABMEmployeeTest` and `MyInfoTest` contain the actual test logic using `[Fact]`
- `BrowserTests.cs` creates `ChromeXxx` and `EdgeXxx` subclasses bound to xUnit collections
- Each browser collection shares one `AuthFixture` (single login per suite)
- Chrome and Edge collections run in **parallel** via xUnit's collection parallelization

## Parallelism

Configured in `xunit.runner.json`:
```json
{
  "parallelizeAssembly": true,
  "parallelizeTestCollections": true,
  "maxParallelThreads": 2
}
```

Chrome and Edge suites run simultaneously. Tests within the same collection run sequentially to share the browser session safely.
