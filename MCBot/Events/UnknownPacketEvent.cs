using System;
using System.IO;

namespace Org.Jonyleeson.MCBot
{
    public delegate void UnknownPacketEventHandler(object sender, UnknownPacketEventArgs e);

    public class UnknownPacketEventArgs : EventArgs
    {
        public BinaryReader Reader
        { get; private set; }

        public byte Opcode
        { get; private set; }

        public UnknownPacketEventArgs(byte opcode, BinaryReader reader)
            : base()
        {
            Opcode = opcode;
            Reader = reader;
        }
    }
}
