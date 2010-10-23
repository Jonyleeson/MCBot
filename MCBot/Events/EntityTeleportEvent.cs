using System;
using Org.Jonyleeson.Generic;

namespace Org.Jonyleeson.MCBot
{
    public delegate void EntityTeleportEventHandler(object sender, EntityTeleportEventArgs e);

    public class EntityTeleportEventArgs : EventArgs
    {
        public int ID
        { get; private set; }

        public Point3D Position
        { get; private set; }

        public byte Yaw
        { get; private set; }

        public byte Pitch
        { get; private set; }

        public EntityTeleportEventArgs(int id, int x, int y, int z, byte yaw, byte pitch)
            : base()
        {
            ID = id;
            Position = new Point3D(x, y, z);
            Yaw = yaw;
            Pitch = pitch;
        }
    }
}
