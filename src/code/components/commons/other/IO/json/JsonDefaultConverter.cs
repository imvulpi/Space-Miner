using Godot;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.commons.other.IO.json
{
    public class JsonDefaultConverter<T> : JsonConverter<T> where T : struct
    {
        public object DefaultValue { get; set; }
        public JsonDefaultConverter(object value)
        {
            DefaultValue = value;
        }
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            try
            {
                var jsonObject = JsonSerializer.Deserialize<T>(ref reader, options);
                GD.Print(jsonObject);
                return jsonObject;
            }
            catch (Exception)
            {
                //GD.Print(DefaultValue);
                return (T)DefaultValue;
            }
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}
