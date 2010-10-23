using System;
using Org.Jonyleeson.Generic;

namespace Org.Jonyleeson.MCBot
{
    public delegate void AddVehicleEventHandler(object sender, AddVehicleEventArgs e);

    public class AddVehicleEventArgs : EventArgs
    {
        public int ID
        { get; private set; }

        public MCVehicleType Type
        { get; private set; }

        public Point3D Position
        { get; private set; }

        public AddVehicleEventArgs(int id, MCVehicleType type, int x, int y, int z)
            : base()
        {
            ID = id;
            Type = type;
            Position = new Point3D(x, y, z);
        }
    }
}
