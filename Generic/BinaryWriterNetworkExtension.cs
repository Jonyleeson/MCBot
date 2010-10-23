using System;
using System.IO;
using System.Net;
using System.Text;

namespace Org.Jonyleeson.MCBot
{
    static class BinaryWriterNetworkExtension // extending binary writer to support big endian
    {
        public static void WriteNetwork(this BinaryWriter writer, short value)
        {
            writer.Write(IPAddress.HostToNetworkOrder(value));
        }

        public static void WriteNetwork(this BinaryWriter writer, int value)
        {
            writer.Write(IPAddress.HostToNetworkOrder(value));
        }

        public static void WriteNetwork(this BinaryWriter writer, long value)
        {
            writer.Write(IPAddress.HostToNetworkOrder(value));
        }

        public static void WriteNetwork(this BinaryWriter writer, string value)
        {
            byte[] data = Encoding.UTF8.GetBytes(value);

            writer.WriteNetwork(((short)data.Length));
            writer.Write(data);
        }

        public static void WriteNetwork(this BinaryWriter writer, double value)
        {
            writer.Write(IPAddress.HostToNetworkOrder(BitConverter.DoubleToInt64Bits(value)));
        }

        public static void WriteNetwork(this BinaryWriter writer, float value)
        {
            writer.Write(IPAddress.HostToNetworkOrder(BitConverter.ToInt32(BitConverter.GetBytes(value), 0)));
        }
    }
}
