namespace NmeLib
{
    public interface ISerializable
    {
        void Write(BulkSerializeWriter writer);
    }
}