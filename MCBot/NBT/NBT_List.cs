using System.Collections.Generic;
using System.IO;
using Org.Jonyleeson.IO;

namespace Org.Jonyleeson.MCBot.NBT
{
    public class NBT_List : INamedBinaryTag
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

        public NBT_Type EntryType
        { get; set; }

        public NBT_List()
        {
            Name = "";
            m_Items = new List<INamedBinaryTag>();
        }

        public NBT_List(BinaryReader reader)
            : this(reader, "")
        { }

        public NBT_List(BinaryReader reader, string name)
        {
            Name = name;
            m_Items = new List<INamedBinaryTag>();

            NBT_Type type = (NBT_Type)reader.ReadByte();
            int len = reader.ReadNetworkInt32();

            EntryType = type;

            // this loop can be optimized by actually switching first then looping, but THAT'S UGLY
            // AND I DON'T LIKE UGLY CODE, EVEN IF IT'S FASTER

            for (int i = 0; i < len; i++)
            {
                switch (type)
                {
                    case NBT_Type.Byte:
                        m_Items.Add(new NBT_Byte(reader));
                        break;
                    case NBT_Type.ByteArray:
                        m_Items.Add(new NBT_ByteArray(reader));
                        break;
                    case NBT_Type.Compound:
                        m_Items.Add(new NBT_Compound(reader));
                        break;
                    case NBT_Type.Double:
                        m_Items.Add(new NBT_Double(reader));
                        break;
                    case NBT_Type.End: // Highly unprobable but protocol doesn't exclude it
                        m_Items.Add(new NBT_End());
                        break;
                    case NBT_Type.Float:
                        m_Items.Add(new NBT_Float(reader));
                        break;
                    case NBT_Type.Int:
                        m_Items.Add(new NBT_Int(reader));
                        break;
                    case NBT_Type.List:
                        m_Items.Add(new NBT_List(reader));
                        break;
                    case NBT_Type.Long:
                        m_Items.Add(new NBT_Long(reader));
                        break;
                    case NBT_Type.Short:
                        m_Items.Add(new NBT_Short(reader));
                        break;
                    case NBT_Type.String:
                        m_Items.Add(new NBT_String(reader));
                        break;
                }
            }
        }
    }
}
