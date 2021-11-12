using NmeLib.Jumps;

namespace NmeLib.StateActions
{
    public class SAJump : StateAction
    {
        public string JumpId { get; set; }
        public Jump Jump { get; set; }

        public SAJump()
        {
        }

        internal SAJump(BulkSerializeReader reader) : base(reader)
        {
            JumpId = reader.ReadString();
            Jump = Jump.Read(reader);
        }

        public override void Write(BulkSerializeWriter writer)
        {
            base.Write(writer);
            writer.Write(JumpId);
            writer.Write(Jump);
        }
    }
}