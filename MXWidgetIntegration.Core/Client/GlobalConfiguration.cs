using System.Collections.Generic;

namespace MXWidgetIntegration.Core.Client
{
    public partial class GlobalConfiguration: Configuration
    {
        private static readonly object GlobalConfigSync = new { };
        private static IReadableConfiguration _globalConfiguration;
        

        public static IReadableConfiguration Instance
        {
            get => _globalConfiguration;
            set
            {
                lock (GlobalConfigSync)
                {
                    _globalConfiguration = value;
                }
            }
        }

        private GlobalConfiguration()
        {
        }
        public GlobalConfiguration(IDictionary<string, string> defaultHeader, IDictionary<string, string> apiKey, IDictionary<string, string> apiKeyPrefix, string basePath = "http://localhost:3000/api") : base(defaultHeader, apiKey, apiKeyPrefix, basePath)
        {
        }
        static GlobalConfiguration()
        {
            Instance = new GlobalConfiguration();
        }
    }
}