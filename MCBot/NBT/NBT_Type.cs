using System;

namespace Org.Jonyleeson.MCBot.NBT
{
    public enum NBT_Type : byte
    {
        End = 0,
        Byte,
        Short,
        Int,
        Long,
        Float,
        Double,
        ByteArray,
        String,
        List,
        Compound
    }
}
