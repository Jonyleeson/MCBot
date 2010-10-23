using System;
using Org.Jonyleeson.Generic;

namespace Org.Jonyleeson.MCBot
{
    public delegate void PreChunkEventHandler(object sender, PreChunkEventArgs e);

    public class PreChunkEventArgs : EventArgs
    {
        public Point3D Reference
        { get; private set; }

        public bool Load
        { get; private set; }

        public PreChunkEventArgs(int x, int z, bool mode)
            : base()
        {
            Reference = new Point3D(x * 16, 0, z * 16);
            Load = mode;
        }
    }
}
