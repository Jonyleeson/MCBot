using System.IO;
using System.IO.Compression;

namespace Org.Jonyleeson.IO.Compression
{
    static class Compression
    {
        public static byte[] DecompressZLib(byte[] zip)
        {
            MemoryStream ms = new MemoryStream(zip);

            ms.Position += 2;

            using (DeflateStream stream = new DeflateStream(ms, CompressionMode.Decompress))
            {
                const int size = 4096;
                byte[] buffer = new byte[size];
                using (MemoryStream memory = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        count = stream.Read(buffer, 0, size);
                        if (count > 0)
                        {
                            memory.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    return memory.ToArray();
                }
            }
        }

        public static byte[] DecompressGZip(byte[] zip)
        {
            MemoryStream ms = new MemoryStream(zip);

            using (GZipStream stream = new GZipStream(ms, CompressionMode.Decompress))
            {
                const int size = 4096;
                byte[] buffer = new byte[size];
                using (MemoryStream memory = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        count = stream.Read(buffer, 0, size);
                        if (count > 0)
                        {
                            memory.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    return memory.ToArray();
                }
            }
        }

    }
}
