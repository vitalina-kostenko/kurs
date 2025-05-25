using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using ToDoList.Models;

namespace ToDoList.Converters
{
    public class JsonTaskItemConverter : JsonConverter<TaskItem>
    {
        public override TaskItem Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var doc = JsonDocument.ParseValue(ref reader);
            var root = doc.RootElement;

            if (!root.TryGetProperty("Type", out var typeProperty))
            {
                try
                {
                    return JsonSerializer.Deserialize<BasicTask>(root.GetRawText(), options)!;
                }
                catch
                {
                    throw new JsonException("The 'Type' property is missing in the JSON data, і не вдалося визначити тип задачі.");
                }
            }

            var typeName = typeProperty.GetString();

            if (typeName == nameof(BasicTask))
                return JsonSerializer.Deserialize<BasicTask>(root.GetRawText(), options)!;

            if (typeName == nameof(TimedTask))
                return JsonSerializer.Deserialize<TimedTask>(root.GetRawText(), options)!;

            throw new JsonException($"Невідомий тип задачі: {typeName}");
        }

        public override void Write(Utf8JsonWriter writer, TaskItem value, JsonSerializerOptions options)
        {
            var json = JsonSerializer.Serialize(value, value.GetType(), options);

            using var doc = JsonDocument.Parse(json);

            writer.WriteStartObject();

            foreach (var prop in doc.RootElement.EnumerateObject())
            {
                prop.WriteTo(writer);
            }

            writer.WriteString("Type", value.GetType().Name);

            writer.WriteEndObject();
        }
    }

}
