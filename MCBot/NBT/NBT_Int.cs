using System.IO;
using Org.Jonyleeson.IO;

namespace Org.Jonyleeson.MCBot.NBT
{
    public class NBT_Int : INamedBinaryTag
    {
        public string Name
        { get; set; }

        public int Value
        { get; set; }

        public NBT_Int()
        {
            Name = "";
            Value = 0;
        }

        public NBT_Int(BinaryReader reader)
            : this(reader, "")
        { }

        public NBT_Int(BinaryReader reader, string name)
        {
            Name = name;
            Value = reader.ReadNetworkInt32();
        }
    }
}
