using System;
using System.Text.Json.Serialization;

namespace JsonEnumTest
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface, AllowMultiple = false)]
    public class FallbackJsonConverterAttribute : JsonConverterAttribute
    {
        private object value;
        private JsonConverter converter;
        public FallbackJsonConverterAttribute(Type converterType, object defaultValue)
        {
            value = defaultValue;
            CreateConverter(converterType);
        }

        public override JsonConverter? CreateConverter(Type typeToConvert)
        {
            if(typeToConvert == null)
            {
                return null;
            }else if(converter != null)
            {
                return converter;
            }

            object instance = Activator.CreateInstance(typeToConvert, value);

            if (instance != null && instance is JsonConverter jsonConverter)
            {
                converter = jsonConverter;
                return jsonConverter;
            }
            return null;
        }
    }
}
