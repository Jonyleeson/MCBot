using System;
using Org.Jonyleeson.Generic;

namespace Org.Jonyleeson.MCBot
{
    public delegate void ItemSpawnEventHandler(object sender, ItemSpawnEventArgs e);

    public class ItemSpawnEventArgs : EventArgs
    {
        public int ID
        { get; private set; }

        public short Item
        { get; private set; }

        public byte Count
        { get; private set; }

        public Point3D Position
        { get; private set; }

        public byte Yaw
        { get; private set; }

        public byte Pitch
        { get; private set; }

        public byte Roll
        { get; private set; }

        public ItemSpawnEventArgs(int id, short item, byte count, int x, int y, int z, byte yaw, byte pitch, byte roll)
            : base()
        {
            ID = id;
            Item = item;
            Count = count;
            Position = new Point3D(x / 32f, y / 32f, z / 32f);
            Yaw = yaw;
            Pitch = pitch;
            Roll = roll;
        }
    }
}
