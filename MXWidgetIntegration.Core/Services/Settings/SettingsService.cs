using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MXWidgetIntegration.Core.Services.Settings
{
    public static class SettingsService
    {
        private const string _ClientId = "25f81b9f-f8fc-42d9-bb9c-661fdd987115";
        private const string _ApiKey = "f1a3b84b5ff6c84c76bcdec0a36c84e17c59e8f4";
        private static string _Base64Authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_ClientId}:{_ApiKey}"));
        private const string _baseAddress = "https://int-api.mx.com/";
        public static string ClientId => _ClientId;
        public static string ApiKey => _ApiKey;
        public static string Base64Authorization => _Base64Authorization;
        public static string BaseAddress => _baseAddress;
    }
}
