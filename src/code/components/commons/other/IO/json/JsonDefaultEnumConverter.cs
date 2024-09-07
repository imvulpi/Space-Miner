using Godot;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.commons.errors.logging;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SpaceMiner.src.code.components.commons.other.IO.json
{
    public class JsonDefaultEnumConverter<T> : JsonConverter<T> where T : struct, Enum
    {
        public object DefaultValue { get; set; }
        public JsonDefaultEnumConverter(object value)
        {
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

                PrettyLogger.Log(PrettyInfoType.RepairAttempt, $"JSON {enumString} value repair attempt");
                T? repairedValue = TryFindValue(enumString);
                if (repairedValue != null)
                {
                    PrettyLogger.Log(PrettyInfoType.Success, $"JSON Value Repair", $"Succesfully repaired JSON value {enumString} to {repairedValue}");
                    return (T)repairedValue;
                }
                else
                {
                    PrettyLogger.Log(PrettyErrorType.OperationFailed, "JSON Value Repair", $"Could not repair {enumString} JSON value.");
                    PrettyLogger.Log(PrettyErrorType.Invalid, $"Invalid value ({enumString})", $"Value ({enumString}) is invalid ({typeToConvert.Name})");
                    PrettyLogger.Log(PrettyInfoType.Defaulted, $"{typeToConvert.Name}", "Value was defaulted");
                    return (T)DefaultValue;
                }
            }
            else
            {
                PrettyLogger.Log(PrettyErrorType.Invalid, $"Invalid value", $"Invalid {typeToConvert.Name} value, should be a string.");
                PrettyLogger.Log(PrettyInfoType.Defaulted, $"{typeToConvert.Name}", "Value was defaulted");
                return (T)DefaultValue;
            }
        }

        private T? TryFindValue(string invalidValue)
        {
            string[] types = Enum.GetNames<T>();
            foreach (string type in types)
            {
                if (type.Contains(invalidValue) && Enum.TryParse(type, out T result))
                {
                    return result;
                }
            }
            return null;
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
