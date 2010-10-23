using System;
using System.Collections.Generic;

namespace Org.Jonyleeson.MCBot
{
    public delegate void PlayerInventoryEventHandler(object sender, PlayerInventoryEventArgs e);

    public class PlayerInventoryEventArgs : EventArgs
    {
        public MCInventoryType Inventory
        { get; private set; }

        public MCItem[] Items
        { get; private set; }

        public PlayerInventoryEventArgs(MCInventoryType type, List<MCItem> items)
            : base()
        {
            Inventory = type;
            Items = items.ToArray();
        }
    }
}
