using System;

namespace Org.Jonyleeson.MCBot
{
    public delegate void DisconnectEventHandler(object sender, DisconnectEventArgs e);

    public class DisconnectEventArgs : EventArgs
    {
        public string Reason
        { get; private set; }

        public DisconnectEventArgs(string reason)
            : base()
        {
            Reason = reason;
        }
    }
}
