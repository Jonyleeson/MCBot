using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Org.Jonyleeson.MCBot.NBT
{
    public class NBT_Byte : INamedBinaryTag
    {
        public string Name
        { get; set; }

        public byte Value
        { get; set; }

        public NBT_Byte()
        {
            Name = "";
            Value = 0;
        }

        public NBT_Byte(BinaryReader reader)
            : this(reader, "")
        { }

        public NBT_Byte(BinaryReader reader, string name)
        {
            Name = name;
            Value = reader.ReadByte();
        }
    }
}
