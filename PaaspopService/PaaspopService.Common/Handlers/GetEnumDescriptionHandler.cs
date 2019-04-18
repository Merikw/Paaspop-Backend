using System;
using System.ComponentModel;

//source: https://stackoverflow.com/questions/1415140/can-my-enums-have-friendly-names
namespace PaaspopService.Common.Handlers
{
    public static class GetEnumDescriptionHandler
    {
        public static string GetDescription(this Enum value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            if (name == null) return null;
            var field = type.GetField(name);
            if (field == null) return null;
            var attribute =
                Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attribute?.Description;
        }
    }
}
