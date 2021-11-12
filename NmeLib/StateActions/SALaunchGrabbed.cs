namespace NmeLib.StateActions
{
    public class SALaunchGrabbed : StateAction
    {
        public string AtkProp { get; set; }

        public SALaunchGrabbed()
        {
        }

        internal SALaunchGrabbed(BulkSerializeReader reader) : base(reader)
        {
            AtkProp = reader.ReadString();
        }

        public override void Write(BulkSerializeWriter writer)
        {
            base.Write(writer);
            writer.Write(AtkProp);
        }
    }
}