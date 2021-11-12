﻿namespace NmeLib.FloatSources
{
    public class FSSports : FloatSource
    {
        public Attributes Attribute { get; set; }

        public FSSports()
        {
        }

        internal FSSports(BulkSerializeReader reader) : base(reader)
        {
            Attribute = (Attributes)reader.ReadInt();
        }

        public override void Write(BulkSerializeWriter writer)
        {
            base.Write(writer);
            writer.Write(Attribute);
        }

        public enum Attributes
        {
            ActiveBall,
            BounceOnGoalEdge,
            GoalScore,
            RespawnBall,
            InheritPush,
            LastBallPushX,
            LastBallPushY
        }
    }
}