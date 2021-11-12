using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Newtonsoft.Json;
using NmeLib.CheckThings;

namespace NmeLib.Converters
{
    public class CheckThingConverter : JsonConverter<CheckThing>
    {
        private static readonly PropertyInfo tidInfo = typeof(CheckThing).GetProperty(nameof(CheckThing.TID));
        private static readonly PropertyInfo versionInfo = typeof(CheckThing).GetProperty(nameof(CheckThing.Version));

        public override CheckThing ReadJson(JsonReader reader, Type objectType, CheckThing existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return Converter.ReadJson<CheckThing.TypeId, CheckThing>(reader, serializer, tidInfo, versionInfo, res => res switch
            {
                CheckThing.TypeId.MultipleId => new CTMultiple(),
                CheckThing.TypeId.CompareId => new CTCompareFloat(),
                CheckThing.TypeId.DoubleTapId => new CTDoubleTapId(),
                CheckThing.TypeId.InputId => new CTInput(),
                CheckThing.TypeId.InputSeriesId => new CTInputSeries(),
                CheckThing.TypeId.TechId => new CTCheckTech(),
                CheckThing.TypeId.GrabId => new CTGrabId(),
                CheckThing.TypeId.GrabAgentId => new CTGrabbedAgent(),
                CheckThing.TypeId.SkinId => new CTSkin(),
                CheckThing.TypeId.MoveId => new CTMove(),
                CheckThing.TypeId.BaseIdentifier => new CheckThing(),
                _ => throw new JsonException(),
            });
        }

        public override void WriteJson(JsonWriter writer, CheckThing value, JsonSerializer serializer)
        {
            Converter.WriteJson(writer, value, serializer, tidInfo, versionInfo);
        }
    }
}