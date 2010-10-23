using System.IO;
using Org.Jonyleeson.IO;

namespace Org.Jonyleeson.MCBot.NBT
{
    public class NBT_Float : INamedBinaryTag
    {
        public string Name
        { get; set; }

        public float Value
        { get; set; }

        public NBT_Float()
        {
            Name = "";
            Value = 0f;
        }

        public NBT_Float(BinaryReader reader)
            : this(reader, "")
        { }

        public NBT_Float(BinaryReader reader, string name)
        {
            Name = name;
            Value = reader.ReadNetworkSingle();
        }
    }
}
