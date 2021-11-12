namespace NmeLib.StateActions
{
    public class SASetupHurtboxes : StateAction
    {
        public HurtSetSetup HurtSetSetup { get; set; }

        public SASetupHurtboxes()
        {
        }

        internal SASetupHurtboxes(BulkSerializeReader reader) : base(reader)
        {
            HurtSetSetup = new HurtSetSetup(reader);
        }

        public override void Write(BulkSerializeWriter writer)
        {
            base.Write(writer);
            writer.Write(HurtSetSetup);
        }
    }
}