using System;

namespace Org.Jonyleeson.MCBot
{
    public class MCItem
    {
        public MCBlockType Type
        { get; set; }
        public byte Count
        { get; set; }
        public short Health
        { get; set; }

        public MCItem(short id, byte count, short health)
        {
            Type = (MCBlockType)id;
            Count = count;
            Health = health;
        }
    }
}
