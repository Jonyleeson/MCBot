using System;
using Org.Jonyleeson.MCBot.NBT;
using Org.Jonyleeson.Generic;

namespace Org.Jonyleeson.MCBot
{
    public delegate void ComplexEntitiesEventHandler(object sender, ComplexEntitiesEventArgs e);

    public class ComplexEntitiesEventArgs : EventArgs
    {
        public Point3D Position
        { get; private set; }

        public INamedBinaryTag NBT
        { get; private set; }

        public ComplexEntitiesEventArgs(int x, short y, int z, INamedBinaryTag tag)
            : base()
        {
            Position = new Point3D(x, y, z);
            NBT = tag;
        }
    }
}
