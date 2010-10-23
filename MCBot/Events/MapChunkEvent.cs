using System;
using System.IO;
using Org.Jonyleeson.Generic;

namespace Org.Jonyleeson.MCBot
{
    public delegate void MapChunkEventHandler(object sender, MapChunkEventArgs e);

    public class MapChunkEventArgs : EventArgs
    {
        public Point3D Position
        { get; private set; }

        public MCBlock[,,] ChunkData
        { get; private set; }

        public MapChunkEventArgs(int x, short y, int z, MCBlock[,,] data)
            : base()
        {
            Position = new Point3D(x, y, z);
            ChunkData = data;
        }
    }
}
