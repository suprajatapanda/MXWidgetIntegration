using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MXWidgetIntegration.Core.Client
{
    public interface IApiAccessor
    {
        IReadableConfiguration Configuration { get; set; }
        string GetBasePath();
        ExceptionFactory ExceptionFactory { get; set; }
    }
}
