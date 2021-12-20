using System;
using System.Collections.Generic;
using System.Net;

namespace MXWidgetIntegration.Core.Client
{
    public class ApiResponse<T> : IApiResponse
    {
        public T Data { get; }
        public Type ResponseType => typeof(T);
        public object Content => Data;
        public HttpStatusCode StatusCode { get; }
        public Multimap<string, string> Headers { get; }
        public string ErrorText { get; set; }
        public List<Cookie> Cookies { get; set; }
        public string RawContent { get; }

        public ApiResponse(HttpStatusCode statusCode, Multimap<string, string> headers, T data, string rawContent)
        {
            StatusCode = statusCode;
            Headers = headers;
            Data = data;
            RawContent = rawContent;
        }
        public ApiResponse(HttpStatusCode statusCode, Multimap<string, string> headers, T data) : this(statusCode, headers, data, null)
        {
        }
        public ApiResponse(HttpStatusCode statusCode, T data, string rawContent) : this(statusCode, null, data, rawContent)
        {
        }
        public ApiResponse(HttpStatusCode statusCode, T data) : this(statusCode, data, null)
        {
        }
    }
}