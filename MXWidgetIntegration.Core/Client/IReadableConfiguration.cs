using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace MXWidgetIntegration.Core.Client
{
    public interface IReadableConfiguration
    {
        string AccessToken { get; }
        IDictionary<string, string> ApiKey { get; }
        IDictionary<string, string> ApiKeyPrefix { get; }
        string BasePath { get; }      
        IDictionary<string, string> DefaultHeaders { get; }
        string TempFolderPath { get; }
        int Timeout { get; }
        WebProxy Proxy { get; }
        string UserAgent { get; }
        string Username { get; }
        string Password { get; }
        string GetApiKeyWithPrefix(string apiKeyIdentifier);
        X509CertificateCollection ClientCertificates { get; }
        string DateTimeFormat { get;}
    }
}