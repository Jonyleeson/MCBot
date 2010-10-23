using System;

namespace Org.Jonyleeson.MCBot
{
    public delegate void HandshakeEventHandler(object sender, HandshakeEventArgs e);

    public class HandshakeEventArgs : EventArgs
    {
        public string Hash
        { get; private set; }

        public HandshakeEventArgs(string hash)
            : base()
        {
            Hash = hash;
        }
    }
}
