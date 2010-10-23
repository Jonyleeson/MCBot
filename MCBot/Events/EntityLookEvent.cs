using System;

namespace Org.Jonyleeson.MCBot
{
    public delegate void EntityLookEventHandler(object sender, EntityLookEventArgs e);

    public class EntityLookEventArgs : EventArgs
    {
        public int ID
        { get; private set; }

        public byte Yaw
        { get; private set; }

        public byte Pitch
        { get; private set; }

        public EntityLookEventArgs(int id, byte yaw, byte pitch)
            : base()
        {
            ID = id;
            Yaw = yaw;
            Pitch = pitch;
        }
    }
}
