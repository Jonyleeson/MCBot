using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace Org.Jonyleeson.MCBot
{
    public delegate void PacketEventHandler(object sender, PacketRecieveEventArgs e);

    class MCSocket
    {
        TcpClient m_Connection;
        Thread m_RecieveLoop;

        public event PacketEventHandler OnRecieve;

        public bool Connected
        {
            get
            { return m_Connection.Connected; }
        }

        public MCSocket(string server, int port)
        {
            m_Connection = new TcpClient(server, port);
            m_RecieveLoop = new Thread(new ThreadStart(delegate { RecieveLoop(); }));
            m_RecieveLoop.Start();
        }

        public void SendPacket(byte[] packet)
        {
            if (m_Connection.Connected)
            {
                try
                {
                    m_Connection.GetStream().Write(packet, 0, 1);
                    m_Connection.GetStream().Flush();
                    m_Connection.GetStream().Write(packet, 1, packet.Length - 1);
                }
                catch
                { }
            }
        }

        public void Disconnect(string message)
        {
            this.SendPacket(MCPackets.CreateDisconnectPacket(message));
            m_RecieveLoop.Abort();
            m_Connection.Close();
        }

        private void RecieveLoop()
        {
            byte[] packet;
            MemoryStream ms = new MemoryStream();
            
            while (true)
            {
                if (m_Connection.Available > 0)
                {
                    if (OnRecieve != null)
                    {
                        packet = new byte[m_Connection.Available];
                            
                        try
                        {
                            m_Connection.GetStream().Read(packet, 0, packet.Length);
                        }
                        catch // connection is closed or something, continue into other handler
                        {
                            continue;
                        }

                        long pos = ms.Position;
                        ms.Position = ms.Length;
                        ms.Write(packet, 0, packet.Length);
                        ms.Position = pos;
                    }
                }

                if (OnRecieve != null && ms.Length > 0)
                {
                    long pos = ms.Position;

                    try
                    {
                        OnRecieve(this, new PacketRecieveEventArgs(new BinaryReader(ms)));
                    }
                    catch
                    {
                        ms.Position = pos;
                    }
                }

                if (ms.Position == ms.Length)
                {
                    ms.Close();
                    ms.Dispose();

                    ms = new MemoryStream();
                }

                Thread.Sleep(1);
            }
        }
    }
}
