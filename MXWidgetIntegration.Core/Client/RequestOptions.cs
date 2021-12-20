using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace MXWidgetIntegration.Core.Client
{
    public class RequestOptions
    {
        public Dictionary<string, string> PathParameters { get; set; }
        public Multimap<string, string> QueryParameters { get; set; }
        public Multimap<string, string> HeaderParameters { get; set; }
        public Dictionary<string, string> FormParameters { get; set; }
        public Dictionary<string, Stream> FileParameters { get; set; }
        public List<Cookie> Cookies { get; set; }
        public object Data { get; set; }
        public RequestOptions()
        {
            PathParameters = new Dictionary<string, string>();
            QueryParameters = new Multimap<string, string>();
            HeaderParameters = new Multimap<string, string>();
            FormParameters = new Dictionary<string, string>();
            FileParameters = new Dictionary<string, Stream>();
            Cookies = new List<Cookie>();
        }
    }
}