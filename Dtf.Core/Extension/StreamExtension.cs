using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Dtf.Core
{
    public static class StreamExtension
    {
        public const int BufferSize = 1024;

        public static string ReadUTF8String(this Stream stream, int length)
        {
            byte[] data = new byte[length];
            int readed = 0;
            do
            {
                int count = stream.Read(data, readed, length-readed);
                readed += count;
            } while (readed < length);
            return UTF8Encoding.UTF8.GetString(data, 0, data.Length);
        }

        public static int WriteUTF8String(this Stream stream, string content)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(content);
            int length = data.Length;
            stream.Write(data, 0, length);
            return length;
        }

        /// <summary>
        /// ReadString using BinaryReader
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string ReadStringEnd(this Stream stream)
        {
            BinaryReader binaryReader = new BinaryReader(stream);
            return binaryReader.ReadString();
        }

        /// <summary>
        /// Write string using BinaryWriter (start with data size)
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="text"></param>
        public static void WriteStringEnd(this Stream stream, string text)
        {
            BinaryWriter binaryWriter = new BinaryWriter(stream);
            binaryWriter.Write(text);
        }
    }
}
