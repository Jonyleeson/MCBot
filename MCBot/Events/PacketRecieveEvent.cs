using System;
using System.IO;

namespace Org.Jonyleeson.MCBot
{
    public class PacketRecieveEventArgs : EventArgs
    {
        public BinaryReader Reader
        { get; private set; }

        public PacketRecieveEventArgs(BinaryReader reader) : base()
        {
            Reader = reader;
        }
    }
}
