using System;
using Org.Jonyleeson.Generic;

namespace Org.Jonyleeson.MCBot
{
    public delegate void BlockChangeEventHandler(object sender, BlockChangeEventArgs e);

    public class BlockChangeEventArgs : EventArgs
    {
        public Point3D Position
        { get; private set; }

        public MCBlockType Block
        { get; private set; }

        public byte MetaData
        { get; private set; }

        public BlockChangeEventArgs(int x, byte y, int z, byte type, byte metadata)
            : base()
        {
            Position = new Point3D(x, y, z);
            Block = (MCBlockType)type;
            MetaData = metadata;
        }
    }
}
