using NmeLib.CheckThings;

namespace NmeLib.StateActions
{
    public class SACheckThing : StateAction
    {
        public CheckThing CheckThing { get; set; }
        public StateAction Action { get; set; }
        public bool Else { get; set; }
        public StateAction ElseAction { get; set; }

        public SACheckThing()
        {
        }

        internal SACheckThing(BulkSerializeReader reader) : base(reader)
        {
            CheckThing = CheckThing.Read(reader);
            Action = Read(reader);
            Else = reader.ReadBool();
            ElseAction = Read(reader);
        }

        public override void Write(BulkSerializeWriter writer)
        {
            base.Write(writer);
            writer.Write(CheckThing);
            writer.Write(Action);
            writer.Write(Else);
            writer.Write(ElseAction);
        }
    }
}