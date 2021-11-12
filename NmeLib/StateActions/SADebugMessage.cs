﻿namespace NmeLib.StateActions
{
    public class SADebugMessage : StateAction
    {
        public string Message { get; set; }

        public SADebugMessage()
        {
        }

        public SADebugMessage(BulkSerializeReader reader) : base(reader)
        {
            Message = reader.ReadString();
        }

        public override void Write(BulkSerializeWriter writer)
        {
            base.Write(writer);
            writer.Write(Message);
        }
    }
}