using System;
using Org.Jonyleeson.Generic;

namespace Org.Jonyleeson.MCBot
{
    public delegate void SpawnPositionEventHandler(object sender, SpawnPositionEventArgs e);

    public class SpawnPositionEventArgs : EventArgs
    {
        public Point3D Position
        { get; private set; }

        public SpawnPositionEventArgs(int x, int y, int z)
            : base()
        {
            Position = new Point3D(x, y, z);
        }
    }
}
