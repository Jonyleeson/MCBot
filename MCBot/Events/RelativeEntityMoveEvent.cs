using System;
using Org.Jonyleeson.Generic;

namespace Org.Jonyleeson.MCBot
{
    public delegate void RelativeEntityMoveEventHandler(object sender, RelativeEntityMoveEventArgs e);

    public class RelativeEntityMoveEventArgs : EventArgs
    {
        public int ID
        { get; private set; }

        public Vector3D Translation
        { get; private set; }

        public RelativeEntityMoveEventArgs(int id, byte x, byte y, byte z)
            : base()
        {
            ID = id;
            Translation = new Vector3D(x, y, z);
        }
    }
}
