using System;

namespace Org.Jonyleeson.MCBot
{
    public delegate void HoldSwitchEventHandler(object sender, HoldSwitchEventArgs e);

    public class HoldSwitchEventArgs : EventArgs
    {
        public int ID
        { get; private set; }

        public MCBlockType Item
        { get; private set; }

        public HoldSwitchEventArgs(int entity, short item)
            : base()
        {
            ID = entity;
            Item = (MCBlockType)item;
        }
    }
}
