using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Semptra.SalesforceDotNet
{
    internal static class JsonHelpers
    {
        internal static IEnumerable<string> GetJsonProperties(Type type)
        {
            return type.GetProperties()
                 .Select(x => x.GetCustomAttribute<JsonPropertyAttribute>())
                 .Where(x => x != null)
                 .Select(x => x.PropertyName);
        }
    }
}
