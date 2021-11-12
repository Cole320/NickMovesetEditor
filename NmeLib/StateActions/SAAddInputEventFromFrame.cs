﻿namespace NmeLib.StateActions
{
    public class SAAddInputEventFromFrame : StateAction
    {
        public GIEV AddEvent { get; set; }

        public SAAddInputEventFromFrame()
        {
        }

        internal SAAddInputEventFromFrame(BulkSerializeReader reader) : base(reader)
        {
            AddEvent = (GIEV)reader.ReadInt();
        }

        public override void Write(BulkSerializeWriter writer)
        {
            base.Write(writer);
            writer.Write(AddEvent);
        }
    }
}