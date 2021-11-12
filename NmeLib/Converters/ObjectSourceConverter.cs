using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Newtonsoft.Json;
using NmeLib.ObjectSources;

namespace NmeLib.Converters
{
    public class ObjectSourceConverter : JsonConverter<ObjectSource>
    {
        private static readonly PropertyInfo tidInfo = typeof(ObjectSource).GetProperty(nameof(ObjectSource.TID));
        private static readonly PropertyInfo versionInfo = typeof(ObjectSource).GetProperty(nameof(ObjectSource.Version));

        public override ObjectSource ReadJson(JsonReader reader, Type objectType, ObjectSource existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return Converter.ReadJson<ObjectSource.TypeId, ObjectSource>(reader, serializer, tidInfo, versionInfo, res => res switch
            {
                ObjectSource.TypeId.FloatId => new OSFloat(),
                ObjectSource.TypeId.Vector2Id => new OSVector2(),
                ObjectSource.TypeId.BaseIdentifier => new ObjectSource(),
                _ => throw new JsonException(),
            });
        }

        public override void WriteJson(JsonWriter writer, ObjectSource value, JsonSerializer serializer)
        {
            Converter.WriteJson(writer, value, serializer, tidInfo, versionInfo);
        }
    }
}