﻿namespace NmeLib.FloatSources
{
    public class FSPhysics : FloatSource
    {
        public PhysicsAttributes Attribute { get; set; }

        public FSPhysics()
        {
        }

        internal FSPhysics(BulkSerializeReader reader) : base(reader)
        {
            Attribute = (PhysicsAttributes)reader.ReadInt();
        }

        public override void Write(BulkSerializeWriter writer)
        {
            base.Write(writer);
            writer.Write(Attribute);
        }

        public enum PhysicsAttributes
        {
            Gravity
        }
    }
}