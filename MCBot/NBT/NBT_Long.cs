using System.IO;
using Org.Jonyleeson.IO;

namespace Org.Jonyleeson.MCBot.NBT
{
    public class NBT_Long : INamedBinaryTag
    {
        public string Name
        { get; set; }

        public long Value
        { get; set; }

        public NBT_Long()
        {
            Name = "";
            Value = 0L;
        }

        public NBT_Long(BinaryReader reader)
            : this(reader, "")
        { }

        public NBT_Long(BinaryReader reader, string name)
        {
            Name = name;
            Value = reader.ReadNetworkInt64();
        }
    }
}
