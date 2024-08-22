using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonEnumTest
{
    public class NullableEnumConverter<T> : JsonConverter<T?> where T : struct, Enum
    {
        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            if (reader.TokenType == JsonTokenType.String)
            {
                string enumString = reader.GetString();
                if (Enum.TryParse<T>(enumString, ignoreCase: true, out var enumValue) && Enum.IsDefined(typeof(T), enumValue))
                {
                    return enumValue;
                }
            }

            return null;
        }

        public override void Write(Utf8JsonWriter writer, T? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                writer.WriteStringValue(value.Value.ToString());
            }
            else
            {
                writer.WriteNullValue();
            }
        }
    }

}
