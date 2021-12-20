using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace MXWidgetIntegration.Core.Model
{
    public abstract partial class JsonSerializer
    {
        static public readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            MissingMemberHandling = MissingMemberHandling.Error,
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy
                {
                    OverrideSpecifiedNames = false
                }
            }
        };        
        public abstract string ToJson();
    }
}
