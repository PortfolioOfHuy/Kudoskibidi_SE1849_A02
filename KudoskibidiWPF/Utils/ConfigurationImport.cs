using Microsoft.Extensions.Configuration;
using System.IO;

namespace KudoskibidiWPF.Utils
{
    public static class ConfigurationImport
    {
        private static readonly IConfigurationRoot _config;
        static ConfigurationImport()
        {
            _config = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                      .Build();
        }

        public static string AdminEmail => _config["AdminAccount:Email"];
        public static string AdminPassword => _config["AdminAccount:Password"];
    }
}
