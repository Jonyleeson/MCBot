using System.IO;
using Org.Jonyleeson.IO;

namespace Org.Jonyleeson.MCBot.NBT
{
    public class NBT_String : INamedBinaryTag
    {
        public string Name
        { get; set; }

        public string Value
        { get; set; }

        public NBT_String()
        {
            Name = "";
            Value = "";
        }

        public NBT_String(BinaryReader reader)
            : this(reader, "")
        { }

        public NBT_String(BinaryReader reader, string name)
        {
            Name = name;
            Value = reader.ReadNetworkString();
        }
    }
}
