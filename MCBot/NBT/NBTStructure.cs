using System.IO;
using Org.Jonyleeson.IO;

namespace Org.Jonyleeson.MCBot.NBT
{
    public static class NBTStructure
    {
        public static INamedBinaryTag ParseNBT(byte[] nbt)
        {
            INamedBinaryTag r = null;

            BinaryReader reader = new BinaryReader(new MemoryStream(nbt));

            // Read initial named tag

            NBT_Type type = (NBT_Type)reader.ReadByte();
            string name = reader.ReadNetworkString();

            // Should be compound, but best be sure

            switch (type)
            {
                case NBT_Type.Compound:
                    r = new NBT_Compound(reader, name);
                    break;
                case NBT_Type.Byte:
                    r = new NBT_Byte(reader, name);
                    break;
                case NBT_Type.ByteArray:
                    r = new NBT_ByteArray(reader, name);
                    break;
                case NBT_Type.Double:
                    r = new NBT_Double(reader, name);
                    break;
                case NBT_Type.End:
                    r = new NBT_End(name);
                    break;
                case NBT_Type.Float:
                    r = new NBT_Float(reader, name);
                    break;
                case NBT_Type.Int:
                    r = new NBT_Int(reader, name);
                    break;
                case NBT_Type.List:
                    r = new NBT_List(reader, name);
                    break;
                case NBT_Type.Long:
                    r = new NBT_Long(reader, name);
                    break;
                case NBT_Type.Short:
                    r = new NBT_Short(reader, name);
                    break;
                case NBT_Type.String:
                    r = new NBT_String(reader, name);
                    break;
            }

            return r;
        }
    }
}
