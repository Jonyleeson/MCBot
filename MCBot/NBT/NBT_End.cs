using System;

namespace Org.Jonyleeson.MCBot.NBT
{
    class NBT_End : INamedBinaryTag // One might find this odd, but it turns out the protocol doesn't explicitly
    {                               // mention that TAG_Ends cannot be used in TAG_Lists
        public string Name
        { get; set; }

        public object Value // required by interface
        { 
            get
            { 
                return null; 
            }
        }

        public NBT_End()
        {
            Name = "";
        }

        public NBT_End(string name)
        {
            Name = name;
        }
    }
}
