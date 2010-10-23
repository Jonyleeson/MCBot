using System;
using Org.Jonyleeson.Generic;

namespace Org.Jonyleeson.MCBot
{
    public delegate void PlayerMoveLookEventHandler(object sender, PlayerMoveLookEventArgs e);

    public class PlayerMoveLookEventArgs : EventArgs
    {
        public Point3D Position
        { get; private set; }

        public double Stance
        { get; private set; }

        public float Yaw
        { get; private set; }

        public float Pitch
        { get; private set; }

        public bool Ground
        { get; private set; }

        public PlayerMoveLookEventArgs(double x, double y, double stance, double z, float yaw, float pitch, bool ground)
            : base()
        {
            Position = new Point3D(x, y, z);
            Stance = stance;
            Yaw = yaw;
            Pitch = pitch;
            Ground = ground;
        }
    }
}
