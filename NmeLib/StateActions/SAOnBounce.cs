namespace NmeLib.StateActions
{
    public class SAOnBounce : StateAction
    {
        public StateAction Action { get; set; }

        public SAOnBounce()
        {
        }

        internal SAOnBounce(BulkSerializeReader reader) : base(reader)
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