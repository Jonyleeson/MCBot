using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Org.Jonyleeson.IO;

namespace Org.Jonyleeson.MCBot.NBT
{
    public class NBT_ByteArray : INamedBinaryTag
    {
        public string Name
        { get; set; }

        public byte[] Value
        { get; set; }

        public NBT_ByteArray()
        {
            Name = "";
            Value = null;
        }

        public NBT_ByteArray(BinaryReader reader)
            : this(reader, "")
        { }

        public NBT_ByteArray(BinaryReader reader, string name)
        {
            Name = name;
            Value = reader.ReadBytes(reader.ReadNetworkInt32());
        }
    }
}
