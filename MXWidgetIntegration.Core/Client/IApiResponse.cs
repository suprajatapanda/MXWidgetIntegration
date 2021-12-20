using System;
using System.Collections.Generic;
using System.Net;

namespace MXWidgetIntegration.Core.Client
{
    public interface IApiResponse
    {
        Type ResponseType { get; }
        Object Content { get; }
        HttpStatusCode StatusCode { get; }
        Multimap<string, string> Headers { get; }
        string ErrorText { get; set; }
        List<Cookie> Cookies { get; set; }
        string RawContent { get; }

    }
}