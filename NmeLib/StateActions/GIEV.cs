namespace NmeLib.StateActions
{
    public enum GIEV
    {
        None,
        Unplugged,
        ActionFromFrame,
        SpecialFromFrame = 4,
        OffenseFromFrame = 8,
        DefenseFromFrame = 16,
        JumpFromFrame = 32,
        Grounded = 64
    }
}