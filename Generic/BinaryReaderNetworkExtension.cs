using System;
using System.IO;
using System.Net;
using System.Text;

namespace Org.Jonyleeson.IO
{
    static class BinaryReaderNetworkExtension // extending binary reader to support big endian
    {
        public static short ReadNetworkInt16(this BinaryReader reader)
        {
            return IPAddress.NetworkToHostOrder(reader.ReadInt16());
        }

        public static int ReadNetworkInt32(this BinaryReader reader)
        {
            return IPAddress.NetworkToHostOrder(reader.ReadInt32());
        }

        public static long ReadNetworkInt64(this BinaryReader reader)
        {
            return IPAddress.NetworkToHostOrder(reader.ReadInt64());
        }

        public static string ReadNetworkString(this BinaryReader reader)
        {
            return Encoding.UTF8.GetString(reader.ReadBytes(reader.ReadNetworkInt16()));
        }

        public static double ReadNetworkDouble(this BinaryReader reader)
        {
            return BitConverter.Int64BitsToDouble(reader.ReadNetworkInt64());
        }

        public static float ReadNetworkSingle(this BinaryReader reader)
        {
            return BitConverter.ToSingle(BitConverter.GetBytes(reader.ReadNetworkInt32()), 0);
        }
    }
}
