namespace NmeLib.StateActions
{
    public class SAManageAirJump : StateAction
    {
        public ManageType Manage { get; set; }

        public SAManageAirJump()
        {
        }

        internal SAManageAirJump(BulkSerializeReader reader) : base(reader)
        {
            Manage = (ManageType)reader.ReadInt();
        }

        public override void Write(BulkSerializeWriter writer)
        {
            base.Write(writer);
            writer.Write(Manage);
        }

        public enum ManageType
        {
            ExpendAirJump,
            ResetAirJumps,
            ExpendAirDash,
            ResetAirDashes
        }
    }
}