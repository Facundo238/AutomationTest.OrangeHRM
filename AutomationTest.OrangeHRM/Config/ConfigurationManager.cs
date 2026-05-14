using Microsoft.Extensions.Configuration;

namespace OrangeHRM.AutomationTests.Config
{
    public static class ConfigurationManager
    {
        private static readonly IConfiguration Configuration;

        static ConfigurationManager()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public static string BaseUrl => 
            Configuration["OrangeHRM:BaseUrl"] 
            ?? throw new InvalidOperationException("BaseUrl not configured in appsettings.json");

        public static (string Username, string Password) GetUser(string account) =>
            (Configuration[$"OrangeHRM:Users:{account}:Username"]
                ?? throw new InvalidOperationException($"Username for account '{account}' not configured"),
             Configuration[$"OrangeHRM:Users:{account}:Password"]
                ?? throw new InvalidOperationException($"Password for account '{account}' not configured"));

        public static string DbConnectionString =>
            Configuration["ConnectionStrings:OrangeHRM"]
            ?? throw new InvalidOperationException("ConnectionStrings:OrangeHRM not configured in appsettings.json");
    }
}
