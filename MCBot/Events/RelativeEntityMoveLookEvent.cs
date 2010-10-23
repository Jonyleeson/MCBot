using System;
using Org.Jonyleeson.Generic;

namespace Org.Jonyleeson.MCBot
{
    public delegate void RelativeEntityMoveLookEventHandler(object sender, RelativeEntityMoveLookEventArgs e);

    public class RelativeEntityMoveLookEventArgs : EventArgs
    {
        public int ID
        { get; private set; }

        public Vector3D Translation
        { get; private set; }

        public byte Yaw
        { get; private set; }

        public byte Pitch
        { get; private set; }

        public RelativeEntityMoveLookEventArgs(int id, byte x, byte y, byte z, byte yaw, byte pitch)
            : base()
        {
            ID = id;
            Translation = new Vector3D(x, y, z);
            Yaw = yaw;
            Pitch = pitch;
        }
    }
}
