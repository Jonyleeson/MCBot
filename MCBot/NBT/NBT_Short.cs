using System.IO;
using Org.Jonyleeson.IO;

namespace Org.Jonyleeson.MCBot.NBT
{
    public class NBT_Short : INamedBinaryTag
    {
        public string Name
        { get; set; }

        public short Value
        { get; set; }

        public NBT_Short()
        {
            Name = "";
            Value = 0;
        }

        public NBT_Short(BinaryReader reader)
            : this(reader, "")
        { }

        public NBT_Short(BinaryReader reader, string name)
        {
            Name = name;
            Value = reader.ReadNetworkInt16();
        }
    }
}
