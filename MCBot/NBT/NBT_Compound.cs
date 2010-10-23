using System.Collections.Generic;
using System.IO;
using Org.Jonyleeson.IO;

namespace Org.Jonyleeson.MCBot.NBT
{
    public class NBT_Compound : INamedBinaryTag
    {
        private List<INamedBinaryTag> m_Items;

        public string Name
        { get; set; }

        public INamedBinaryTag[] Value
        {
            get
            {
                return m_Items.ToArray();
            }
            set
            {
                m_Items.Clear();
                m_Items.AddRange(value);
            }
        }

        public NBT_Compound()
        {
            Name = "";
            m_Items = new List<INamedBinaryTag>();
        }

        public NBT_Compound(BinaryReader reader)
            : this(reader, "")
        { }

        public NBT_Compound(BinaryReader reader, string name)
        {
            Name = name;
            m_Items = new List<INamedBinaryTag>();

            for (NBT_Type type = (NBT_Type)reader.ReadByte(); type != NBT_Type.End; type = (NBT_Type)reader.ReadByte())
            {
                string subname = reader.ReadNetworkString();

                switch (type)
                {
                    case NBT_Type.Compound:
                        m_Items.Add(new NBT_Compound(reader, subname));
                        break;
                    case NBT_Type.Byte:
                        m_Items.Add(new NBT_Byte(reader, subname));
                        break;
                    case NBT_Type.ByteArray:
                        m_Items.Add(new NBT_ByteArray(reader, subname));
                        break;
                    case NBT_Type.Double:
                        m_Items.Add(new NBT_Double(reader, subname));
                        break;
                    case NBT_Type.Float:
                        m_Items.Add(new NBT_Float(reader, subname));
                        break;
                    case NBT_Type.Int:
                        m_Items.Add(new NBT_Int(reader, subname));
                        break;
                    case NBT_Type.List:
                        m_Items.Add(new NBT_List(reader, subname));
                        break;
                    case NBT_Type.Long:
                        m_Items.Add(new NBT_Long(reader, subname));
                        break;
                    case NBT_Type.Short:
                        m_Items.Add(new NBT_Short(reader, subname));
                        break;
                    case NBT_Type.String:
                        m_Items.Add(new NBT_String(reader, subname));
                        break;
                }
            }
        }
    }
}
