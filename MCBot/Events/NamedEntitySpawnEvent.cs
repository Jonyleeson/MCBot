using System;
using Org.Jonyleeson.Generic;

namespace Org.Jonyleeson.MCBot
{
    public delegate void NamedEntitySpawnEventHandler(object sender, NamedEntitySpawnEventArgs e);

    public class NamedEntitySpawnEventArgs : EventArgs
    {
        public int ID
        { get; private set; }

        public string Name
        { get; private set; }

        public Point3D Position
        { get; private set; }

        public byte Yaw
        { get; private set; }

        public byte Pitch
        { get; private set; }

        public short Item
        { get; private set; }

        public NamedEntitySpawnEventArgs(int id, string name, int x, int y, int z, byte yaw, byte pitch, short item)
            : base()
        {
            ID = id;
            Name = name;
            Position = new Point3D(x / 32f, y / 32f, z / 32f);
            Yaw = yaw;
            Pitch = pitch;
            Item = item;
        }
    }
}
