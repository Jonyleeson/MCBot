using System.IO;

namespace Org.Jonyleeson.MCBot
{
    static class MCPackets
    {
        public static byte[] CreateHandshakePacket(string username)
        {
            byte[] buffer;

            BinaryWriter writer = new BinaryWriter(new MemoryStream());

            writer.Write((byte)MCPacketOpcodes.Handshake);
            writer.WriteNetwork(username);

            buffer = new byte[writer.BaseStream.Position];

            writer.BaseStream.Position = 0;
            writer.BaseStream.Read(buffer, 0, buffer.Length);
            writer.Close();
            writer.Dispose();

            return buffer;
        }

        public static byte[] CreateLoginPacket(string username, string password)
        {
            byte[] buffer;

            BinaryWriter writer = new BinaryWriter(new MemoryStream());

            writer.Write((byte)MCPacketOpcodes.Login);
            writer.WriteNetwork(2); // Protocol version
            writer.WriteNetwork(username);
            writer.WriteNetwork(password);

            buffer = new byte[writer.BaseStream.Position];

            writer.BaseStream.Position = 0;
            writer.BaseStream.Read(buffer, 0, buffer.Length);
            writer.Close();
            writer.Dispose();

            return buffer;
        }

        public static byte[] CreateChatPacket(string message)
        {
            byte[] buffer;

            BinaryWriter writer = new BinaryWriter(new MemoryStream());

            writer.Write((byte)MCPacketOpcodes.Chat);
            writer.WriteNetwork(message);

            buffer = new byte[writer.BaseStream.Position];

            writer.BaseStream.Position = 0;
            writer.BaseStream.Read(buffer, 0, buffer.Length);
            writer.Close();
            writer.Dispose();

            return buffer;
        }

        public static byte[] CreateHeartbeatPacket()
        {
            byte[] buffer;

            BinaryWriter writer = new BinaryWriter(new MemoryStream());

            writer.Write((byte)MCPacketOpcodes.KeepAlive);

            buffer = new byte[writer.BaseStream.Position];

            writer.BaseStream.Position = 0;
            writer.BaseStream.Read(buffer, 0, buffer.Length);
            writer.Close();
            writer.Dispose();

            return buffer;
        }

        public static byte[] CreateDisconnectPacket(string message)
        {
            byte[] buffer;

            BinaryWriter writer = new BinaryWriter(new MemoryStream());

            writer.Write((byte)MCPacketOpcodes.Disconnect);
            writer.WriteNetwork(message);

            buffer = new byte[writer.BaseStream.Position];

            writer.BaseStream.Position = 0;
            writer.BaseStream.Read(buffer, 0, buffer.Length);
            writer.Close();
            writer.Dispose();

            return buffer;
        }

        public static byte[] CreatePlayerMoveLookPacket(double x, double y, double stance, double z, float yaw, float pitch, bool ground)
        {
            byte[] buffer;

            BinaryWriter writer = new BinaryWriter(new MemoryStream());

            writer.Write((byte)MCPacketOpcodes.PlayerMoveLook);
            writer.WriteNetwork(x);
            writer.WriteNetwork(y);
            writer.WriteNetwork(stance);
            writer.WriteNetwork(z);
            writer.WriteNetwork(yaw);
            writer.WriteNetwork(pitch);
            writer.Write(ground);

            buffer = new byte[writer.BaseStream.Position];

            writer.BaseStream.Position = 0;
            writer.BaseStream.Read(buffer, 0, buffer.Length);
            writer.Close();
            writer.Dispose();

            return buffer;
        }

        public static byte[] CreatePlayerLookPacket(float yaw, float pitch, bool ground)
        {
            byte[] buffer;

            BinaryWriter writer = new BinaryWriter(new MemoryStream());

            writer.Write((byte)MCPacketOpcodes.PlayerLook);
            writer.WriteNetwork(yaw);
            writer.WriteNetwork(pitch);
            writer.Write(ground);

            buffer = new byte[writer.BaseStream.Position];

            writer.BaseStream.Position = 0;
            writer.BaseStream.Read(buffer, 0, buffer.Length);
            writer.Close();
            writer.Dispose();

            return buffer;
        }

        public static byte[] CreatePlayerPositionPacket(double x, double y, double stance, double z, bool ground)
        {
            byte[] buffer;

            BinaryWriter writer = new BinaryWriter(new MemoryStream());

            writer.Write((byte)MCPacketOpcodes.PlayerPosition);
            writer.WriteNetwork(x);
            writer.WriteNetwork(y);
            writer.WriteNetwork(stance);
            writer.WriteNetwork(z);
            writer.Write(ground);

            buffer = new byte[writer.BaseStream.Position];

            writer.BaseStream.Position = 0;
            writer.BaseStream.Read(buffer, 0, buffer.Length);
            writer.Close();
            writer.Dispose();

            return buffer;
        }

        public static byte[] CreatePlayerInventoryPacket(MCInventoryType type, MCItem[] items)
        {
            byte[] buffer;

            BinaryWriter writer = new BinaryWriter(new MemoryStream());

            writer.Write((byte)MCPacketOpcodes.PlayerInventory);
            writer.WriteNetwork((int)type);
            writer.WriteNetwork((short)items.Length);

            foreach (MCItem item in items)
            {
                writer.WriteNetwork((short)item.Type);

                if (item.Type != MCBlockType.None)
                {
                    writer.Write(item.Count);
                    writer.WriteNetwork(item.Health);
                }
            }

            buffer = new byte[writer.BaseStream.Position];

            writer.BaseStream.Position = 0;
            writer.BaseStream.Read(buffer, 0, buffer.Length);
            writer.Close();
            writer.Dispose();

            return buffer;
        }

        public static byte[] CreatePlayerTickPacket(bool ground)
        {
            byte[] buffer;

            BinaryWriter writer = new BinaryWriter(new MemoryStream());

            writer.Write((byte)MCPacketOpcodes.PlayerTick);
            writer.Write(ground);

            buffer = new byte[writer.BaseStream.Position];

            writer.BaseStream.Position = 0;
            writer.BaseStream.Read(buffer, 0, buffer.Length);
            writer.Close();
            writer.Dispose();

            return buffer;
        }

        public static byte[] CreatePlayerDiggingPacket(byte status, int x, byte y, int z, byte face)
        {
            byte[] buffer;

            BinaryWriter writer = new BinaryWriter(new MemoryStream());

            writer.Write((byte)MCPacketOpcodes.BlockDig);
            writer.Write(status);
            writer.WriteNetwork(x);
            writer.Write(y);
            writer.WriteNetwork(z);
            writer.Write(face);

            buffer = new byte[writer.BaseStream.Position];

            writer.BaseStream.Position = 0;
            writer.BaseStream.Read(buffer, 0, buffer.Length);
            writer.Close();
            writer.Dispose();

            return buffer;
        }

        public static byte[] CreatePlayerBlockPlacePacket(short item, int x, byte y, int z, byte face)
        {
            byte[] buffer;

            BinaryWriter writer = new BinaryWriter(new MemoryStream());

            writer.Write((byte)MCPacketOpcodes.BlockPlace);
            writer.WriteNetwork(item);
            writer.WriteNetwork(x);
            writer.Write(y);
            writer.WriteNetwork(z);
            writer.Write(face);

            buffer = new byte[writer.BaseStream.Position];

            writer.BaseStream.Position = 0;
            writer.BaseStream.Read(buffer, 0, buffer.Length);
            writer.Close();
            writer.Dispose();

            return buffer;
        }

        public static byte[] CreatePlayerHoldingPacket(int id, short item)
        {
            byte[] buffer;

            BinaryWriter writer = new BinaryWriter(new MemoryStream());

            writer.Write((byte)MCPacketOpcodes.HoldSwitch);
            writer.WriteNetwork(id);
            writer.WriteNetwork(item);

            buffer = new byte[writer.BaseStream.Position];

            writer.BaseStream.Position = 0;
            writer.BaseStream.Read(buffer, 0, buffer.Length);
            writer.Close();
            writer.Dispose();

            return buffer;
        }

        public static byte[] CreateArmAnimationPacket(int id, bool animate)
        {
            byte[] buffer;

            BinaryWriter writer = new BinaryWriter(new MemoryStream());

            writer.Write((byte)MCPacketOpcodes.ArmAnimation);
            writer.WriteNetwork(id);
            writer.Write(animate);

            buffer = new byte[writer.BaseStream.Position];

            writer.BaseStream.Position = 0;
            writer.BaseStream.Read(buffer, 0, buffer.Length);
            writer.Close();
            writer.Dispose();

            return buffer;
        }
    }
}
