using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Serialization;

namespace PaaspopService.Common.DictionaryAsArrayResolver
{
    public class DictionaryAsArrayResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonContract CreateContract(Type objectType)
        {
            return objectType.GetInterfaces().Any(i => i == typeof(IDictionary) ||
                                                       i.IsGenericType && i.GetGenericTypeDefinition() ==
                                                       typeof(IDictionary<,>))
                ? base.CreateArrayContract(objectType)
                : base.CreateContract(objectType);
        }
    }
}