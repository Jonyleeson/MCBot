using System;
using Org.Jonyleeson.Generic;

namespace Org.Jonyleeson.MCBot
{
    public delegate void MultiBlockChangeEventHandler(object sender, MultiBlockChangeEventArgs e);

    public class MultiBlockChangeEventArgs : EventArgs
    {
        public Point3D Reference
        { get; private set; }

        public MCBlockTransform[] BlockChanges
        { get; private set; }

        public MultiBlockChangeEventArgs(int x, int z, MCBlockTransform[] changes)
            : base()
        {
            Reference = new Point3D(x * 16, 0, z * 16);
            BlockChanges = changes;
        }
    }
}
