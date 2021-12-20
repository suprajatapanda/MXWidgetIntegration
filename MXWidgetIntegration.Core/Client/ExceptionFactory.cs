using System;

namespace MXWidgetIntegration.Core.Client
{
    public delegate Exception ExceptionFactory(string methodName, IApiResponse response);    
}