using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Org.Jonyleeson.IO;

namespace Org.Jonyleeson.MCBot.NBT
{
    public class NBT_Double : INamedBinaryTag
    {
        public string Name
        { get; set; }

        public double Value
        { get; set; }

        public NBT_Double()
        {
            Name = "";
            Value = 0d;
        }

        public NBT_Double(BinaryReader reader)
            : this(reader, "")
        { }

        public NBT_Double(BinaryReader reader, string name)
        {
            Name = name;
            Value = reader.ReadNetworkDouble();
        }
    }
}
