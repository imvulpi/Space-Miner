using Godot;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonEnumTest
{
    public class DefaultEnumConverter<T> : JsonConverter<T> where T : struct, Enum
    {
        public object DefaultValue { get; set; }
        public DefaultEnumConverter(object value) { 
            DefaultValue = value;
        }
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                string enumString = reader.GetString();
                if (Enum.TryParse<T>(enumString, ignoreCase: true, out var enumValue) && Enum.IsDefined(typeof(T), enumValue))
                {
                    return enumValue;
                }
            }
            GD.PushError($"Invalid value on {typeToConvert.FullName}! Trying to default to {DefaultValue}");
            return (T)DefaultValue;
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
