using System;
using Org.Jonyleeson.Generic;

namespace Org.Jonyleeson.MCBot
{
    public delegate void MobSpawnEventHandler(object sender, MobSpawnEventArgs e);

    public class MobSpawnEventArgs : EventArgs
    {
        public int ID
        { get; private set; }

        public byte Type
        { get; private set; }

        public Point3D Position
        { get; private set; }

        public byte Yaw
        { get; private set; }

        public byte Pitch
        { get; private set; }

        public MobSpawnEventArgs(int id, byte type, int x, int y, int z, byte yaw, byte pitch)
            : base()
        {
            ID = id;
            Type = type;
            Position = new Point3D(x, y, z);
            Yaw = yaw;
            Pitch = pitch;
        }
    }
}
