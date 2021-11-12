﻿namespace NmeLib.StateActions
{
    public class SAOnLand : StateAction
    {
        public StateAction Action { get; set; }

        public SAOnLand()
        {
        }

        internal SAOnLand(BulkSerializeReader reader) : base(reader)
        {
            Action = Read(reader);
        }

        public override void Write(BulkSerializeWriter writer)
        {
            base.Write(writer);
            writer.Write(Action);
        }
    }
}