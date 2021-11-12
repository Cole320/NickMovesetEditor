using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Newtonsoft.Json;
using NmeLib.StateActions;

namespace NmeLib.Converters
{
    public class StateActionConverter : JsonConverter<StateAction>
    {
        private static readonly PropertyInfo tidInfo = typeof(StateAction).GetProperty(nameof(StateAction.TID));
        private static readonly PropertyInfo versionInfo = typeof(StateAction).GetProperty(nameof(StateAction.Version));

        public override StateAction ReadJson(JsonReader reader, Type objectType, StateAction existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return Converter.ReadJson<StateAction.TypeId, StateAction>(reader, serializer, tidInfo, versionInfo, res => res switch
                {
                    StateAction.TypeId.DebugId => new SADebugMessage(),
                    StateAction.TypeId.PlayAnimId => new SAPlayAnim(),
                    StateAction.TypeId.RootAnimId => new SAPlayRootAnim(),
                    StateAction.TypeId.SnapAnimWeightsId => new SASnapAnimWeights(),
                    StateAction.TypeId.StandardInputId => new SAStandardInput(),
                    StateAction.TypeId.InputId => new SAInputAction(),
                    StateAction.TypeId.DeactInputId => new SADeactivateInputAction(),
                    StateAction.TypeId.InputEventFromFrameId => new SAAddInputEventFromFrame(),
                    StateAction.TypeId.CancelStateId => new SACancelToState(),
                    StateAction.TypeId.CustomCallId => new SACustomCall(),
                    StateAction.TypeId.TimerId => new SATimedAction(),
                    StateAction.TypeId.OrderId => new SAOrderedSensitive(),
                    StateAction.TypeId.ProxyId => new SAProxyMove(),
                    StateAction.TypeId.CheckId => new SACheckThing(),
                    StateAction.TypeId.ActiveActionId => new SAActiveAction(),
                    StateAction.TypeId.DeactivateActionId => new SADeactivateAction(),
                    StateAction.TypeId.SetFloatId => new SASetFloatTarget(),
                    StateAction.TypeId.OnBounceId => new SAOnBounce(),
                    StateAction.TypeId.OnLeaveEdgeId => new SAOnLeaveEdge(),
                    StateAction.TypeId.OnStoppedAtEdgeId => new SAOnStoppedAtLedge(),
                    StateAction.TypeId.OnLandId => new SAOnLand(),
                    StateAction.TypeId.OnCancelId => new SAOnCancel(),
                    StateAction.TypeId.RefreshAtkId => new SARefreshAttack(),
                    StateAction.TypeId.EndAtkId => new SAEndAttack(),
                    StateAction.TypeId.SetHitboxCountId => new SASetHitboxCount(),
                    StateAction.TypeId.ConfigHitboxId => new SAConfigHitbox(),
                    StateAction.TypeId.SetAtkPropId => new SASetAttackProp(),
                    StateAction.TypeId.ManipHitboxId => new SAManipHitbox(),
                    StateAction.TypeId.UpdateHurtsetId => new SAUpdateHurtboxes(),
                    StateAction.TypeId.SetupHurtsetId => new SASetupHurtboxes(),
                    StateAction.TypeId.ManipHurtboxId => new SAManipHurtbox(),
                    StateAction.TypeId.BoneStateId => new SABoneState(),
                    StateAction.TypeId.BoneScaleId => new SABoneScale(),
                    StateAction.TypeId.SpawnAgentId => new SASpawnAgent(),
                    StateAction.TypeId.LocalFXId => new SALocalFX(),
                    StateAction.TypeId.SpawnFXId => new SASpawnFX(),
                    StateAction.TypeId.HitboxFXId => new SASetHitboxFX(),
                    StateAction.TypeId.SFXId => new SAPlaySFX(),
                    StateAction.TypeId.HitboxSFXId => new SASetHitboxSFX(),
                    StateAction.TypeId.ColorTintId => new SAColorTint(),
                    StateAction.TypeId.FindFloorId => new SAFindFloor(),
                    StateAction.TypeId.HurtGrabbedId => new SAHurtGrabbed(),
                    StateAction.TypeId.LaunchGrabbedId => new SALaunchGrabbed(),
                    StateAction.TypeId.StateCancelGrabbedId => new SAStateCancelGrabbed(),
                    StateAction.TypeId.EndGrabId => new SAEndGrab(),
                    StateAction.TypeId.GrabNotifyEscapeId => new SAGrabNotifyEscape(),
                    StateAction.TypeId.IgnoreGrabbedId => new SAIgnoreGrabbed(),
                    StateAction.TypeId.EventKOId => new SAEventKO(),
                    StateAction.TypeId.EventKOGrabbedId => new SAEventKOGrabbed(),
                    StateAction.TypeId.CameraShakeId => new SACameraShake(),
                    StateAction.TypeId.ResetOnHitId => new SAResetOnHits(),
                    StateAction.TypeId.OnHitId => new SAOnHit(),
                    StateAction.TypeId.FastForwardId => new SAFastForwardState(),
                    StateAction.TypeId.TimingTweakId => new SATimingTweak(),
                    StateAction.TypeId.MapAnimId => new SAMapAnimation(),
                    StateAction.TypeId.AlterMoveDtId => new SAAlterMoveDT(),
                    StateAction.TypeId.AlterMoveVelId => new SAAlterMoveVel(),
                    StateAction.TypeId.SetStagePartId => new SASetStagePart(),
                    StateAction.TypeId.SetStagePartsDefaultId => new SASetStagePartsDefault(),
                    StateAction.TypeId.JumpId => new SAJump(),
                    StateAction.TypeId.StopJumpId => new SAStopJump(),
                    StateAction.TypeId.ManageAirJumpId => new SAManageAirJump(),
                    StateAction.TypeId.LeaveGroundId => new SALeaveGround(),
                    StateAction.TypeId.UnhogEdgeId => new SAUnHogEdge(),
                    StateAction.TypeId.SFXTimelineId => new SAPlaySFXTimeline(),
                    StateAction.TypeId.FindLastHorizontalInputId => new SAFindLastHorizontalInput(),
                    StateAction.TypeId.SetCommandGrab => new SASetCommandGrab(),
                    StateAction.TypeId.CameraPunchId => new SACameraPunch(),
                    StateAction.TypeId.SpawnAgent2Id => new SASpawnAgent2(),
                    StateAction.TypeId.ManipDecorChainId => new SAManipDecorChain(),
                    StateAction.TypeId.UpdateHitboxesId => new SAUpdateHitboxes(),
                    StateAction.TypeId.SampleAnimId => new SASampleAnim(),
                    StateAction.TypeId.ForceExtraInputId => new SAForceExtraInputCheck(),
                    StateAction.TypeId.LaunchGrabbedCustomId => new SALaunchGrabbedCustom(),
                    StateAction.TypeId.BaseIdentifier => new StateAction(),
                    _ => throw new JsonException(),
                });
        }

        public override void WriteJson(JsonWriter writer, StateAction value, JsonSerializer serializer)
        {
            Converter.WriteJson(writer, value, serializer, tidInfo, versionInfo);
        }
    }
}