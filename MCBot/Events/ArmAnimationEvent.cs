using System;

namespace Org.Jonyleeson.MCBot
{
    public delegate void ArmAnimationEventHandler(object sender, ArmAnimationEventArgs e);

    public class ArmAnimationEventArgs : EventArgs
    {
        public int ID
        { get; private set; }

        public bool Forward
        { get; private set; }

        public ArmAnimationEventArgs(int id, bool forward)
            : base()
        {
            ID = id;
            Forward = forward;
        }
    }
}
