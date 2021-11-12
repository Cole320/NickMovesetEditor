using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Newtonsoft.Json;
using NmeLib.FloatSources;

namespace NmeLib.Converters
{
    public class FloatSourceConverter : JsonConverter<FloatSource>
    {
        private static readonly PropertyInfo tidInfo = typeof(FloatSource).GetProperty(nameof(FloatSource.TID));
        private static readonly PropertyInfo versionInfo = typeof(FloatSource).GetProperty(nameof(FloatSource.Version));

        public override FloatSource ReadJson(JsonReader reader, Type objectType, FloatSource existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return Converter.ReadJson<FloatSource.TypeId, FloatSource>(reader, serializer, tidInfo, versionInfo, res => res switch
            {
                FloatSource.TypeId.AgentId => new FSAgent(),
                FloatSource.TypeId.BonesId => new FSBones(),
                FloatSource.TypeId.AttackId => new FSAttack(),
                FloatSource.TypeId.FrameId => new FSFrame(),
                FloatSource.TypeId.InputId => new FSInput(),
                FloatSource.TypeId.FuncId => new FSFunc(),
                FloatSource.TypeId.MovementId => new FSMovement(),
                FloatSource.TypeId.CombatId => new FSCombat(),
                FloatSource.TypeId.GrabsId => new FSGrabs(),
                FloatSource.TypeId.DataId => new FSData(),
                FloatSource.TypeId.ScratchId => new FSScratch(),
                FloatSource.TypeId.AnimId => new FSAnim(),
                FloatSource.TypeId.SpeedId => new FSSpeed(),
                FloatSource.TypeId.PhysicsId => new FSPhysics(),
                FloatSource.TypeId.CollisionId => new FSCollision(),
                FloatSource.TypeId.TimerId => new FSTimer(),
                FloatSource.TypeId.LagId => new FSLag(),
                FloatSource.TypeId.EffectsId => new FSEffects(),
                FloatSource.TypeId.ColorsId => new FSColors(),
                FloatSource.TypeId.OnHitId => new FSOnHit(),
                FloatSource.TypeId.RandomId => new FSRandom(),
                FloatSource.TypeId.CameraId => new FSCameraInfo(),
                FloatSource.TypeId.SportsId => new FSSports(),
                FloatSource.TypeId.Vector2Mag => new FSVector2Mag(),
                FloatSource.TypeId.CPUHelpId => new FSCpuHelp(),
                FloatSource.TypeId.ItemId => new FSItem(),
                FloatSource.TypeId.ModeId => new FSMode(),
                FloatSource.TypeId.JumpsId => new FSJumps(),
                FloatSource.TypeId.RootAnimId => new FSRootAnim(),
                FloatSource.TypeId.FloatId => new FSValue(),
                _ => throw new JsonException(),
            });
        }

        public override void WriteJson(JsonWriter writer, FloatSource value, JsonSerializer serializer)
        {
            Converter.WriteJson(writer, value, serializer, tidInfo, versionInfo);
        }
    }
}