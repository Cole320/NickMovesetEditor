namespace NmeLib.StateActions
{
    public class SALocalFX : StateAction
    {
        public LocalFXAction ActionType { get; set; }
        public string Id { get; set; }

        public SALocalFX()
        {
        }

        internal SALocalFX(BulkSerializeReader reader) : base(reader)
        {
            ActionType = (LocalFXAction)reader.ReadInt();
            Id = reader.ReadString();
        }

        public override void Write(BulkSerializeWriter writer)
        {
            base.Write(writer);
            writer.Write(ActionType);
            writer.Write(Id);
        }

        public enum LocalFXAction
        {
            TurnOn,
            TurnOff,
            Restart,
            RestartAll
        }
    }
}