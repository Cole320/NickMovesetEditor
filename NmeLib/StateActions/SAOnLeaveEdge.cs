namespace NmeLib.StateActions
{
    public class SAOnLeaveEdge : StateAction
    {
        public StateAction Action { get; set; }

        public SAOnLeaveEdge()
        {
        }

        internal SAOnLeaveEdge(BulkSerializeReader reader) : base(reader)
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