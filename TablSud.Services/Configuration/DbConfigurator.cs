using System.IO;
using TablSud.Core.Configuration;

using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace TablSud.Services.Configuration
{
    /// <summary>
    /// Load custom configuration from AppConfig.json
    /// </summary>
    public class DbConfigurator : IDbConfigurator
    {
        private const string AppConfigName = "AppConfig.json";
        private static DbConnectionLink _connectionLink;

        public DbConnectionLink GetConfiguration()
        {
            if (_connectionLink == null)
            {
                foreach (string path in Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "*.json",
                    SearchOption.AllDirectories))
                {
                    if (path.Contains(AppConfigName))
                    {
                        using (StreamReader reader = File.OpenText(path))
                        {
                            string rawConfig = reader.ReadToEnd();
                            _connectionLink = JsonConvert.DeserializeObject<DbConnectionLink>(rawConfig);
                            return _connectionLink;
                        }
                    }
                }
                _connectionLink = DbConnectionLink.Empty;
            }
            return _connectionLink;
        }
    }
}
