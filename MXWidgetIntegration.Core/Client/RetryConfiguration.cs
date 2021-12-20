using Polly;
using RestSharp;

namespace MXWidgetIntegration.Core.Client
{
    public class RetryConfiguration
    {
        public static Policy<IRestResponse> RetryPolicy { get; set; }
        public static AsyncPolicy<IRestResponse> AsyncRetryPolicy { get; set; }
    }
}