using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MXWidgetIntegration.Core.Client
{
    public static class ClientUtils
    {
        public static byte[] ReadAsBytes(Stream inputStream)
        {
            using (var ms = new MemoryStream())
            {
                inputStream.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public static string SanitizeFilename(string filename)
        {
            Match match = Regex.Match(filename, @".*[/\\](.*)$");
            return match.Success ? match.Groups[1].Value : filename;
        }

        public static string ParameterToString(object obj, IReadableConfiguration configuration = null)
        {
            if (obj is DateTime dateTime)
                return dateTime.ToString((configuration ?? GlobalConfiguration.Instance).DateTimeFormat);
            if (obj is DateTimeOffset dateTimeOffset)
                return dateTimeOffset.ToString((configuration ?? GlobalConfiguration.Instance).DateTimeFormat);
            if (obj is bool boolean)
                return boolean ? "true" : "false";
            if (obj is ICollection collection)
                return string.Join(",", collection.Cast<object>());

            return Convert.ToString(obj, CultureInfo.InvariantCulture);
        }
        public static Multimap<string, string> ParameterToMultiMap(string collectionFormat, string name, object value)
        {
            var parameters = new Multimap<string, string>();

            if (value is ICollection collection && collectionFormat == "multi")
            {
                foreach (var item in collection)
                {
                    parameters.Add(name, ParameterToString(item));
                }
            }
            else if (value is IDictionary dictionary)
            {
                if (collectionFormat == "deepObject")
                {
                    foreach (DictionaryEntry entry in dictionary)
                    {
                        parameters.Add(name + "[" + entry.Key + "]", ParameterToString(entry.Value));
                    }
                }
                else
                {
                    foreach (DictionaryEntry entry in dictionary)
                    {
                        parameters.Add(entry.Key.ToString(), ParameterToString(entry.Value));
                    }
                }
            }
            else
            {
                parameters.Add(name, ParameterToString(value));
            }

            return parameters;
        }
        public static string Base64Encode(string text) => Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(text));
        public static string SelectHeaderContentType(string[] contentTypes)
        {
            if (contentTypes.Length == 0)
                return null;

            foreach (var contentType in contentTypes)
            {
                if (IsJsonMime(contentType))
                    return contentType;
            }

            return contentTypes[0]; // use the first content type specified in 'consumes'
        }
        public static string SelectHeaderAccept(string[] accepts)
        {
            if (accepts.Length == 0)
                return null;

            if (accepts.Contains("application/json", StringComparer.OrdinalIgnoreCase))
                return "application/json";

            return string.Join(",", accepts);
        }
        public static readonly Regex JsonRegex = new Regex("(?i)^(application/json|[^;/ \t]+/[^;/ \t]+[+]json)[ \t]*(;.*)?$");
        public static bool IsJsonMime(string mime)
        {
            if (string.IsNullOrWhiteSpace(mime)) return false;

            return JsonRegex.IsMatch(mime) || mime.Equals("application/json-patch+json");
        }
    }
}
