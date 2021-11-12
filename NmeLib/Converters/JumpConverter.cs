using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Newtonsoft.Json;
using NmeLib.Jumps;

namespace NmeLib.Converters
{
    public class JumpConverter : JsonConverter<Jump>
    {
        private static readonly PropertyInfo tidInfo = typeof(Jump).GetProperty(nameof(Jump.TID));
        private static readonly PropertyInfo versionInfo = typeof(Jump).GetProperty(nameof(Jump.Version));

        public override Jump ReadJson(JsonReader reader, Type objectType, Jump existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return Converter.ReadJson<Jump.TypeId, Jump>(reader, serializer, tidInfo, versionInfo, res => res switch
            {
                Jump.TypeId.HeightId => new HeightJump(),
                Jump.TypeId.HoldId => new HoldJump(),
                Jump.TypeId.AirdashId => new AirDashJump(),
                Jump.TypeId.KnockbackId => new KnockbackJump(),
                Jump.TypeId.BaseIdentifier => new Jump(),
                // This is more aggressive than the game parser for better error detection.
                _ => throw new JsonException(),
            });
        }

        public override void WriteJson(JsonWriter writer, Jump value, JsonSerializer serializer)
        {
            Converter.WriteJson(writer, value, serializer, tidInfo, versionInfo);
        }
    }
}