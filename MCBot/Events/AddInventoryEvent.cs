using System;
using Org.Jonyleeson.Generic;

namespace Org.Jonyleeson.MCBot
{
    public delegate void AddInventoryEventHandler(object sender, AddInventoryEventArgs e);

    public class AddInventoryEventArgs : EventArgs
    {
        public MCBlockType Item
        { get; private set; }

        public byte Count
        { get; private set; }

        public short Health
        { get; private set; }

        public AddInventoryEventArgs(short item, byte count, short health)
            : base()
        {
            Item = (MCBlockType)item;
            Count = count;
            Health = health;
        }
    }
}
